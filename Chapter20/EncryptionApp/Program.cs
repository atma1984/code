using System.Security.Cryptography; // Подключаем криптографическую библиотеку для обработки ошибок шифрования (CryptographicException)
using Packt.Shared; // Подключаем пространство имен для использования класса Protector (предположительно для шифрования/дешифрования)
using static System.Console; // Для удобства использования методов Console (Write, WriteLine и т.д.)

// Запрашиваем у пользователя ввод текста для шифрования
Write("Enter a message that you want to encrypt: ");
string? message = ReadLine();  // Читаем сообщение от пользователя

// Запрашиваем у пользователя пароль для шифрования
Write("Enter a password: ");
string? password = ReadLine();  // Читаем пароль от пользователя

// Проверяем, что введены и пароль, и сообщение
if ((password is null) || (message is null))  // Если либо сообщение, либо пароль отсутствуют
{
    WriteLine("Message or password cannot be null.");  // Выводим сообщение об ошибке
    return;  // Завершаем выполнение программы, если введены пустые данные
}

// Если пароль и сообщение корректны, шифруем сообщение с использованием класса Protector
string cipherText = Protector.Encrypt(message, password);  // Получаем зашифрованный текст

// Выводим зашифрованный текст
WriteLine($"Encrypted text: {cipherText}");

// Запрашиваем у пользователя ввод пароля для дешифрования
Write("Enter the password: ");
string? password2Decrypt = ReadLine();  // Читаем второй пароль для дешифровки

// Проверяем, что пароль для дешифрования не пустой
if (password2Decrypt is null)
{
    WriteLine("Password to decrypt cannot be null.");  // Если пароль не введен, выводим сообщение
    return;  // Завершаем выполнение программы
}

// Попытка дешифровать зашифрованное сообщение
try
{
    // Дешифруем сообщение, используя второй введенный пароль
    string clearText = Protector.Decrypt(cipherText, password2Decrypt);  // Дешифруем с использованием класса Protector

    // Выводим расшифрованное сообщение
    WriteLine($"Decrypted text: {clearText}");  // Выводим исходный текст после расшифровки
}
catch (CryptographicException ex)  // Если произошла ошибка криптографии (например, неверный пароль)
{
    // Выводим сообщение о неверном пароле
    WriteLine("{0}\nMore details: {1}",
              arg0: "You entered the wrong password!",  // Основное сообщение
              arg1: ex.Message);  // Дополнительная информация об ошибке
}
catch (Exception ex)  // Если произошла другая ошибка
{
    // Выводим сообщение об ошибке, которая не относится к криптографии
    WriteLine("Non-cryptographic exception: {0}, {1}",
              arg0: ex.GetType().Name,  // Тип ошибки
              arg1: ex.Message);  // Сообщение об ошибке
}
