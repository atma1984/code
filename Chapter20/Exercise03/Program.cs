using System.Xml; // XmlReader — для чтения XML документов.
using Packt.Shared; // Protector — для работы с классом, который включает методы шифрования и хеширования данных.

using static System.Console; // Для упрощенного использования методов Console (например, Write, WriteLine).
using static System.IO.Path; // Для удобства работы с путями файлов.
using static System.Environment; // Для получения информации о текущем рабочем каталоге и других данных окружения.
using System.Security.Cryptography; // Для работы с криптографией (например, для расшифровки данных).

WriteLine("You must enter the correct password to decrypt the document.");
// Информируем пользователя, что он должен ввести правильный пароль для расшифровки данных.

Write("Password: ");
// Запрашиваем ввод пароля для расшифровки.

string? password = ReadLine();
// Читаем введенный пользователем пароль (может быть null, если пользователь ничего не ввел).

List<Customer> customers = new();
// Создаем список клиентов, который будет заполняться информацией из XML.

string xmlFile = Combine(CurrentDirectory, "..", "protected-customers.xml");
// Формируем путь к файлу `protected-customers.xml`, который находится в родительской директории относительно текущей.

if (!File.Exists(xmlFile))
// Проверяем, существует ли файл, если нет, то выводим сообщение и завершаем выполнение.
{
    WriteLine($"{xmlFile} does not exist!");
    // Выводим сообщение, если файл не найден.
    return; // Завершаем выполнение программы.
}

XmlReader xmlReader = XmlReader.Create(xmlFile,
  new XmlReaderSettings { IgnoreWhitespace = true });
// Создаем объект XmlReader для чтения XML файла. Настройка `IgnoreWhitespace = true` позволяет игнорировать пробелы в файле.

while (xmlReader.Read())
// Цикл, который будет читать XML файл до конца.
{
    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "customer"))
    // Если текущий узел - это элемент `<customer>`, продолжаем читать.
    {
        xmlReader.Read(); // Переходим к следующему элементу `<name>`
        string name = xmlReader.ReadElementContentAsString();
        // Читаем содержимое элемента `<name>` (имя клиента).
        string creditcardEncrypted = xmlReader.ReadElementContentAsString();
        // Читаем зашифрованный номер кредитной карты из элемента `<creditcard>`.
        string creditcard = "Unknown credit card.";
        // Переменная для хранения расшифрованного номера карты. Изначально устанавливаем как "Unknown credit card."

        string errorMessage = "Unknown error.";
        // Переменная для хранения сообщения об ошибке, если расшифровка не удалась.

        try
        {
            creditcard = Protector.Decrypt(creditcardEncrypted, password);
            // Пытаемся расшифровать номер кредитной карты с помощью пароля пользователя.
        }
        catch (CryptographicException)
        {
            errorMessage = $"Failed to decrypt {name}'s credit card.";
            // Если расшифровка не удалась, записываем сообщение об ошибке.
        }

        string passwordHashed = xmlReader.ReadElementContentAsString();
        // Читаем хешированный пароль из элемента `<password>`.
        string salt = xmlReader.ReadElementContentAsString();
        // Читаем соль для пароля из элемента `<salt>`.

        // Добавляем нового клиента в список, с расшифрованной кредитной картой (или ошибкой) и хешированным паролем.
        customers.Add(new()
        {
            Name = name,
            CreditCard = creditcard ?? errorMessage, // Если creditcard равен null, то показываем ошибку.
            Password = passwordHashed,
            Salt = salt
        });
    }
}

xmlReader.Close();
// Закрываем объект xmlReader после окончания чтения.

WriteLine();
// Печатаем пустую строку для разделения вывода.

int number = 0;
// Переменная для отслеживания индекса клиента.

WriteLine("    {0,-20} {1,-20}",
  arg0: "Name",
  arg1: "Credit Card");
// Печатаем заголовки столбцов для вывода информации о клиентах: "Name" и "Credit Card".

foreach (Customer c in customers)
// Цикл для вывода информации о каждом клиенте из списка `customers`.
{
    WriteLine("[{0}] {1,-20} {2,-20}",
      arg0: number,
      arg1: c.Name,
      arg2: c.CreditCard);
    // Печатаем индекс клиента, имя и расшифрованный номер кредитной карты (или сообщение об ошибке).

    number++;
    // Увеличиваем счетчик для индекса клиента.
}

WriteLine();
// Печатаем пустую строку для разделения вывода.

Write("Press the number of a customer to log in as: ");
// Запрашиваем у пользователя выбрать клиента для входа по его индексу.

string? customerName = null;
// Переменная для хранения имени выбранного клиента.

try
{
    number = int.Parse(ReadKey().KeyChar.ToString());
    // Читаем номер, выбранный пользователем, и преобразуем его в целое число.
    customerName = customers[number].Name;
    // Извлекаем имя клиента по номеру.
}
catch
{
    WriteLine();
    WriteLine("Not a valid customer selection.");
    // Если номер выбранного клиента невалиден (например, введен неправильный символ), выводим ошибку.
    return; // Завершаем выполнение программы.
}

WriteLine();
// Печатаем пустую строку.

Write($"Enter {customerName}'s password: ");
// Запрашиваем у пользователя ввести пароль для выбранного клиента.

string? attemptPassword = ReadLine();
// Читаем введенный пользователем пароль.

if (Protector.CheckPassword(
  password: attemptPassword,
  salt: customers[number].Salt,
  hashedPassword: customers[number].Password))
// Проверяем, совпадает ли введенный пароль с хешированным паролем клиента с учетом соли.
{
    WriteLine("Correct!");
    // Если пароль верный, выводим сообщение "Correct!".
}
else
{
    WriteLine("Wrong!");
    // Если пароль неверный, выводим сообщение "Wrong!".
}
