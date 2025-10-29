using System.Xml; // XmlWriter — для записи XML документов.
using Packt.Shared; // Protector — для использования класса Protector, который обеспечивает шифрование и хеширование данных.

using static System.Console; // Для упрощенного использования методов Console (например, Write, WriteLine).
using static System.IO.Path; // Для работы с путями файлов.
using static System.Environment; // Для получения информации о текущем рабочем каталоге и других данных окружения.

WriteLine("You must enter a password to encrypt the sensitive data in the document.");
// Выводим сообщение, которое информирует пользователя о необходимости ввода пароля для шифрования данных.

WriteLine("You must enter the same password to decrypt the document later.");
// Выводим сообщение, что тот же пароль будет использован для дешифрования данных позже.

Write("Password: ");
// Запрашиваем ввод пароля у пользователя.

string? password = ReadLine();
// Читаем введенный пароль. Важно: пароль может быть пустым (null), если пользователь ничего не введет.

List<Customer> customers = new()
// Создаем список объектов типа Customer, который будет содержать информацию о клиентах.
{
    new()
    {
        Name = "Bob Smith", // Имя первого клиента.
        CreditCard = "1234-5678-9012-3456", // Номер кредитной карты первого клиента.
        Password = "Pa$$w0rd1", // Пароль первого клиента.
    },
    new()
    {
        Name = "Leslie Knope", // Имя второго клиента.
        CreditCard = "8002-5265-3400-2511", // Номер кредитной карты второго клиента.
        Password = "Pa$$w0rd2", // Пароль второго клиента.
    },
     new()
    {
        Name = "Viktor Popov", // Имя второго клиента.
        CreditCard = "348-5265-3456-2222", // Номер кредитной карты второго клиента.
        Password = "Pa$$w0rd3", // Пароль второго клиента.
    }
};

string xmlFile = Combine(CurrentDirectory, "..", "protected-customers.xml");
// Формируем путь к XML файлу, который будет содержать защищенные данные. Путь указывает на родительскую директорию с текущей.

XmlWriter xmlWriter = XmlWriter.Create(xmlFile,
    new XmlWriterSettings { Indent = true });
// Создаем объект для записи XML в файл. Настройка `Indent = true` добавляет отступы для удобства чтения.

xmlWriter.WriteStartDocument();
// Начинаем запись XML документа.

xmlWriter.WriteStartElement("customers");
// Записываем начальный элемент `<customers>`, который будет корневым элементом XML документа.

foreach (Customer c in customers)
// Для каждого клиента в списке `customers` начинаем цикл.
{
    xmlWriter.WriteStartElement("customer");
    // Записываем элемент `<customer>` для каждого клиента.

    xmlWriter.WriteElementString("name", c.Name);
    // Записываем элемент `<name>`, который содержит имя клиента.

    // Для защиты номера кредитной карты шифруем его с использованием пароля, введенного пользователем.
    xmlWriter.WriteElementString("creditcard",
        Protector.Encrypt(c.CreditCard, password));
    // Используем метод `Encrypt` из класса `Protector` для шифрования номера кредитной карты. Пароль пользователя передается для шифрования.

    // Для защиты пароля, сначала хешируем его с солью.
    User u = Protector.Register(c.Name, c.Password);
    // Регистрируем пользователя с именем и паролем. Метод `Register` генерирует соль и хеширует пароль.

    xmlWriter.WriteElementString("password", u.SaltedHashedPassword);
    // Записываем элемент `<password>`, который содержит хешированный пароль с солью.

    xmlWriter.WriteElementString("salt", u.Salt);
    // Записываем элемент `<salt>`, который содержит соль, использованную для хеширования пароля.

    xmlWriter.WriteEndElement();
    // Закрываем элемент `<customer>`.
}
xmlWriter.WriteEndElement();
// Закрываем элемент `<customers>`.

xmlWriter.WriteEndDocument();
// Закрываем XML документ.

xmlWriter.Close();
// Закрываем объект `xmlWriter` и сохраняем изменения в файл.

WriteLine();
// Печатаем пустую строку для разделения вывода.

WriteLine("Contents of the protected file:");
// Выводим сообщение, информируя о содержимом защищенного файла.

WriteLine();
// Печатаем пустую строку.

WriteLine(File.ReadAllText(xmlFile));
// Читаем содержимое только что созданного XML файла и выводим его в консоль, чтобы показать результат.

