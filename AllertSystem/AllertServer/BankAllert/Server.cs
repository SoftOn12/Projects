using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Controls;
using System.Windows;

namespace BankAllert
{
    class Server
    {
        //static List<TcpClient> clientList = new List<TcpClient>();
        public static string FileName = "Allert.xml";
        static string Path = AppContext.BaseDirectory + @"\" + FileName;
        public static List<Allert> AllertList;
        public static int Port;

        static public void AllertView(ListView AllertListView)
        {
            AllertListView.Items.Clear();
            //Для проверки работы класс аллертов и парсера
            AllertList = ServerUtils.XMLParser(FileName);
            foreach (Allert item in AllertList)
            {
                //item.AllertToConsoleWPF(ConsoleWPF);
                AllertListView.Items.Add(item.TitleName);
            }
            AllertListView.SelectedItem = AllertListView.Items[0];
        }

        static public void ServerStart(TextBox ConsoleWPF)
        {

            string ipString = ServerUtils.GetIp();
            IPAddress localAddr = IPAddress.Parse(ipString);
            TcpListener server = new TcpListener(localAddr, Port);
            server.Start();
            
            while (true)
            {
                //Считываем файл в buffer
                FileStream FileStr = new FileStream(Path, FileMode.Open);
                byte[] buffer = new byte[FileStr.Length];
                int len = (int)FileStr.Length;
                FileStr.Read(buffer, 0, len);
                FileStr.Close();

               // Можно создать текстовый пулл подключенных IP, и вывводить, если в него добавляется/убирается новый IP при каждом запросе
               // Можно сравнивать предыдущий и текущий пакеты и выводить в консоль инфу только о тех отправках, которые несут в себе что-то новое

                // Получаем входящее подключение
                TcpClient client = server.AcceptTcpClient();
                //ConsoleWPF.Text += "Подключен клиент. Выполнение запроса..." + client.Client.LocalEndPoint.ToString();

                // Получаем сетевой поток для чтения и записи
                NetworkStream stream = client.GetStream();
                // отправка сообщения
                stream.Write(buffer, 0, buffer.Length);
                //string MessengeSendString = "Отправлено сообщение клиенту " + client.Client.LocalEndPoint.ToString() +"\n";
                //Application.Current.Dispatcher.BeginInvoke(new Action(delegate () { ConsoleWPF.Text += MessengeSendString; }));

                stream.Close();
                client.Close();
            }
        }
    }
}
