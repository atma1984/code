using System.Xml; // XmlWriter - для работы с XML в формате потока
using System.Text.Json; // Для работы с JSON
using Microsoft.EntityFrameworkCore; // Для работы с EF Core и методом Include
using Packt.Shared; // Для доступа к классам Northwind, Category, Product

using static System.Console; // Для удобства использования методов Console (Write, WriteLine и т.д.)
using static System.IO.Path; // Для удобства работы с путями файлов
using static System.Environment; // Для получения информации о текущей директории

WriteLine("Creating four files containing serialized categories and products..."); // Вывод сообщения в консоль о создании четырех файлов с сериализованными данными


using (Northwind db = new()) // Открываем новый контекст базы данных для работы с ней
{
    // a query to get all categories and their related products 
    IQueryable<Category>? categories = db.Categories?.Include(c => c.Products); // Выполняем запрос на получение всех категорий и связанных с ними продуктов, включая коллекцию продуктов с помощью метода Include

    if (categories is null) // Проверка на случай, если категорий не было найдено
    {
        WriteLine("No categories found."); // Выводим сообщение, если категорий не найдено
        return; // Завершаем выполнение программы
    }

    // Далее генерируем файлы с различными форматами сериализованных данных
    GenerateXmlFile(categories); // Генерируем XML файл, используя элементы для хранения данных
    GenerateXmlFile(categories, useAttributes: false); // Генерируем другой XML файл, но используя атрибуты вместо элементов
    GenerateCsvFile(categories); // Генерируем CSV файл
    GenerateJsonFile(categories); // Генерируем JSON файл
}


static void GenerateXmlFile(
  IQueryable<Category> categories, bool useAttributes = true) // Метод для генерации XML файла, который принимает категорию и флаг, определяющий использование аттрибутов или элементов для записи
{
    string which = useAttributes ? "attibutes" : "elements"; // Строка, которая указывает, используются ли аттрибуты или элементы в XML

    string xmlFile = $"categories-and-products-using-{which}.xml"; // Формируем имя XML файла в зависимости от использования аттрибутов или элементов

    using (FileStream xmlStream = File.Create(
      Combine(CurrentDirectory, xmlFile))) // Создаем файл в текущей директории для записи данных
    {
        using (XmlWriter xml = XmlWriter.Create(xmlStream,
          new XmlWriterSettings { Indent = true })) // Создаем XMLWriter для записи в файл с настройкой отступов для улучшения читаемости
        {

            WriteDataDelegate writeMethod; // Делегат, который будет указывать, какой метод использовать для записи данных (как аттрибуты или элементы)

            if (useAttributes) // Если флаг useAttributes равен true, используем аттрибуты
            {
                writeMethod = xml.WriteAttributeString; // Устанавливаем метод для записи аттрибутов
            }
            else // Иначе используем элементы
            {
                writeMethod = xml.WriteElementString; // Устанавливаем метод для записи элементов
            }

            xml.WriteStartDocument(); // Начинаем запись XML документа
            xml.WriteStartElement("categories"); // Начинаем запись корневого элемента <categories>

            foreach (Category c in categories) // Проходим по всем категориям
            {
                xml.WriteStartElement("category"); // Начинаем запись элемента <category>
                writeMethod("id", c.CategoryId.ToString()); // Записываем аттрибут или элемент с id категории
                writeMethod("name", c.CategoryName); // Записываем аттрибут или элемент с названием категории
                writeMethod("desc", c.Description); // Записываем аттрибут или элемент с описанием категории
                writeMethod("product_count", c.Products.Count.ToString()); // Записываем аттрибут или элемент с количеством продуктов в категории
                xml.WriteStartElement("products"); // Начинаем запись элемента <products>

                foreach (Product p in c.Products) // Проходим по всем продуктам в категории
                {
                    xml.WriteStartElement("product"); // Начинаем запись элемента <product>

                    writeMethod("id", p.ProductId.ToString()); // Записываем аттрибут или элемент с id продукта
                    writeMethod("name", p.ProductName); // Записываем аттрибут или элемент с названием продукта
                    writeMethod("cost", p.Cost is null ? "0" : p.Cost.Value.ToString()); // Записываем аттрибут или элемент с ценой продукта, если цена null, записываем 0
                    writeMethod("stock", p.Stock.ToString()); // Записываем аттрибут или элемент с количеством на складе
                    writeMethod("discontinued", p.Discontinued.ToString()); // Записываем аттрибут или элемент с флагом о прекращении производства

                    xml.WriteEndElement(); // Закрываем элемент <product>
                }
                xml.WriteEndElement(); // Закрываем элемент <products>
                xml.WriteEndElement(); // Закрываем элемент <category>
            }
            xml.WriteEndElement(); // Закрываем корневой элемент <categories>
        }
    }

    WriteLine("{0} contains {1:N0} bytes.",
      arg0: xmlFile,
      arg1: new FileInfo(xmlFile).Length); // Выводим информацию о размере созданного файла
}


static void GenerateCsvFile(IQueryable<Category> categories)
{
    string csvFile = "categories-and-products.csv"; // Формируем имя CSV файла

    using (FileStream csvStream = File.Create(Combine(CurrentDirectory, csvFile))) // Создаем файл в текущей директории
    {
        using (StreamWriter csv = new(csvStream)) // Создаем поток для записи в CSV файл
        {
            csv.WriteLine("CategoryId,CategoryName,Description,ProductId,ProductName,Cost,Stock,Discontinued"); // Записываем заголовок CSV файла

            foreach (Category c in categories) // Проходим по всем категориям
            {
                foreach (Product p in c.Products) // Проходим по всем продуктам в категории
                {
                    csv.Write("{0},\"{1}\",\"{2}\",", // Записываем данные категории
                      arg0: c.CategoryId,
                      arg1: c.CategoryName,
                      arg2: c.Description);

                    csv.Write("{0},\"{1}\",{2},", // Записываем данные продукта
                      arg0: p.ProductId,
                      arg1: p.ProductName,
                      arg2: p.Cost is null ? 0 : p.Cost.Value); // Если цена null, записываем 0

                    csv.WriteLine("{0},{1}", // Записываем оставшиеся данные о продукте
                      arg0: p.Stock,
                      arg1: p.Discontinued); // Записываем данные о наличии и статусе снятия с производства
                }
            }
        }
    }

    WriteLine("{0} contains {1:N0} bytes.", // Выводим информацию о размере созданного файла
      arg0: csvFile,
      arg1: new FileInfo(csvFile).Length);
}


static void GenerateJsonFile(IQueryable<Category> categories)
{
    string jsonFile = "categories-and-products.json"; // Формируем имя JSON файла

    using (FileStream jsonStream = File.Create(Combine(CurrentDirectory, jsonFile))) // Создаем файл для записи JSON
    {
        using (Utf8JsonWriter json = new(jsonStream,
          new JsonWriterOptions { Indented = true })) // Создаем поток для записи JSON с настройкой отступов для улучшенной читаемости
        {
            json.WriteStartObject(); // Начинаем запись JSON объекта
            json.WriteStartArray("categories"); // Начинаем запись массива категорий

            foreach (Category c in categories) // Проходим по всем категориям
            {
                json.WriteStartObject(); // Начинаем запись объекта категории

                json.WriteNumber("id", c.CategoryId); // Записываем id категории
                json.WriteString("name", c.CategoryName); // Записываем имя категории
                json.WriteString("desc", c.Description); // Записываем описание категории
                json.WriteNumber("product_count", c.Products.Count); // Записываем количество продуктов в категории

                json.WriteStartArray("products"); // Начинаем запись массива продуктов

                foreach (Product p in c.Products) // Проходим по всем продуктам
                {
                    json.WriteStartObject(); // Начинаем запись объекта продукта

                    json.WriteNumber("id", p.ProductId); // Записываем id продукта
                    json.WriteString("name", p.ProductName); // Записываем имя продукта
                    json.WriteNumber("cost", p.Cost is null ? 0 : p.Cost.Value); // Записываем стоимость продукта, если null — 0
                    json.WriteNumber("stock", p.Stock is null ? 0 : p.Stock.Value); // Записываем количество на складе
                    json.WriteBoolean("discontinued", p.Discontinued); // Записываем, снят ли продукт с производства

                    json.WriteEndObject(); // Закрываем объект продукта
                }
                json.WriteEndArray(); // Закрываем массив продуктов
                json.WriteEndObject(); // Закрываем объект категории
            }
            json.WriteEndArray(); // Закрываем массив категорий
            json.WriteEndObject(); // Закрываем основной объект JSON
        }
    }

    WriteLine("{0} contains {1:N0} bytes.", // Выводим информацию о размере созданного файла
      arg0: jsonFile,
      arg1: new FileInfo(jsonFile).Length);
}


delegate void WriteDataDelegate(string name, string? value); // Определение делегата, который будет использоваться для записи данных в XML (либо как аттрибуты, либо как элементы)