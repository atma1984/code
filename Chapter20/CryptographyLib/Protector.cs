using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using static System.Console;
using static System.Convert;

namespace Packt.Shared
{
    public static class Protector
    {
        // Размер соли должен быть не менее 8 байт. Мы используем 16 байт.
        // Соль помогает обеспечить уникальность каждого шифрования, даже если используется одинаковый пароль.
        private static readonly byte[] salt =
            Encoding.Unicode.GetBytes("7BANANAS"); // Преобразуем строку в байты с использованием кодировки Unicode.

        private static Dictionary<string, User> Users = new();

        // Количество итераций должно быть достаточно высоким, чтобы
        // на генерацию ключа и IV на целевой машине уходило не менее 100 мс.
        // Например, 150 000 итераций занимают 139 мс на процессоре Intel Core i7-1165G7.
        private static readonly int iterations = 150_000; // Количество итераций для генерации ключа и IV.


        public static User Register(string username, string password)
        {
            // генерируем случайную соль
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            string saltText = ToBase64String(saltBytes);
            // генерируем соленый и хешированный пароль
            string saltedhashedPassword = SaltAndHashPassword(password, saltText);
            User user = new(username, saltText, saltedhashedPassword);
            Users.Add(user.Name, user);
            return user;
        }


        // проверяем пароль пользователя, который хранится
        // в приватном статическом словаре Users
        public static bool CheckPassword(string username, string password)
        {
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            User u = Users[username];
            return CheckPassword(password,
            u.Salt, u.SaltedHashedPassword);
        }

        public static bool CheckPassword(string password,string salt, string hashedPassword)
        {
            // повторно генерируем соленый и хешированный пароль
            string saltedhashedPassword = SaltAndHashPassword(  password, salt);
          
            return (saltedhashedPassword == hashedPassword);
        }

        private static string SaltAndHashPassword(string password, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string saltedPassword = password + salt;
                return ToBase64String(sha.ComputeHash(
                Encoding.Unicode.GetBytes(saltedPassword)));
            }
        }


        // Метод для шифрования текста
        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes; // Массив для хранения зашифрованных данных.
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText); // Преобразуем исходный текст в байты.

            // Используем встроенную реализацию AES для шифрования.
            using (Aes aes = Aes.Create()) // Создаем экземпляр AES с фабричным методом.
            {
                // Засекаем время, которое потребуется для генерации ключа и IV.
                Stopwatch timer = Stopwatch.StartNew();

                // Используем PBKDF2 для генерации ключа и IV на основе пароля и соли.
                using (Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations))
                {
                    // Генерируем 256-битный ключ и 128-битный вектор инициализации (IV).
                    aes.Key = pbkdf2.GetBytes(32); // 32 байта для ключа AES-256.
                    aes.IV = pbkdf2.GetBytes(16); // 16 байт для IV (AES использует 128-битный IV).
                }

                // Останавливаем таймер после завершения генерации ключа и IV.
                timer.Stop();

                // Выводим информацию о времени, затраченном на генерацию ключа и IV.
                WriteLine("{0:N0} milliseconds to generate Key and IV using {1:N0} iterations.",
                          arg0: timer.ElapsedMilliseconds, arg1: iterations);

                // Стартуем поток для шифрования.
                using (MemoryStream ms = new()) // Память для хранения зашифрованных данных.
                {
                    // Создаем шифратор на основе AES.
                    using (ICryptoTransform transformer = aes.CreateEncryptor())
                    {
                        // Создаем поток шифрования.
                        using (CryptoStream cs = new(ms, transformer, CryptoStreamMode.Write))
                        {
                            // Пишем данные для шифрования в поток.
                            cs.Write(plainBytes, 0, plainBytes.Length);
                        }
                    }

                    // Сохраняем зашифрованные данные в массив байт.
                    encryptedBytes = ms.ToArray();
                }
            }

            // Возвращаем зашифрованные данные в формате Base64.
            return ToBase64String(encryptedBytes);
        }

        // Метод для дешифрования текста
        public static string Decrypt(string cipherText, string password)
        {
            byte[] plainBytes; // Массив для хранения расшифрованных данных.
            byte[] cryptoBytes = FromBase64String(cipherText); // Преобразуем зашифрованные данные из Base64 в байты.

            // Используем встроенную реализацию AES для дешифрования.
            using (Aes aes = Aes.Create()) // Создаем экземпляр AES с фабричным методом.
            {
                // Используем PBKDF2 для генерации ключа и IV на основе пароля и соли.
                using (Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations))
                {
                    // Генерируем 256-битный ключ и 128-битный вектор инициализации (IV).
                    aes.Key = pbkdf2.GetBytes(32); // 32 байта для ключа AES-256.
                    aes.IV = pbkdf2.GetBytes(16); // 16 байт для IV (AES использует 128-битный IV).
                }

                // Стартуем поток для дешифрования.
                using (MemoryStream ms = new()) // Память для хранения расшифрованных данных.
                {
                    // Создаем дешифратор на основе AES.
                    using (ICryptoTransform transformer = aes.CreateDecryptor())
                    {
                        // Создаем поток дешифрования.
                        using (CryptoStream cs = new(ms, transformer, CryptoStreamMode.Write))
                        {
                            // Пишем зашифрованные данные в поток для дешифровки.
                            cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                        }
                    }

                    // Сохраняем расшифрованные данные в массив байт.
                    plainBytes = ms.ToArray();
                }
            }

            // Возвращаем расшифрованный текст, преобразованный обратно в строку с использованием Unicode.
            return Encoding.Unicode.GetString(plainBytes);
        }
    }
}
