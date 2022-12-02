using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows;

namespace ClientAllert
{
    class Client
    {
        public static string FileName = "Allert.xml";
        static string Path = AppContext.BaseDirectory + @"\" + FileName;
        public static List<Allert> AllertList;
        static public void AllertView(ListView AllertListView)
        {
            AllertListView.Items.Clear();
            //Для проверки работы класс аллертов и парсера
            AllertList = ClientUtils.XMLParser(FileName);
            foreach (Allert item in AllertList)
            {
                //item.AllertToConsoleWPF(ConsoleWPF);
                AllertListView.Items.Add(item.TitleName);
            }
            AllertListView.SelectedItem = AllertListView.Items[0];
        }

        static public void ClientStart(TextBox Status)
        {
            //Создание объекта, для работы с файлом
            IniManager MyIni = new IniManager("config.ini");

            string server = MyIni.Read("address");
            int port = Convert.ToInt32(MyIni.Read("port"));

            List<Allert> AllertListOld;
            List<Allert> AllertListNew;
            string FirstString = "Подключение к " + server + ":" + port.ToString() + "\n";
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate () { Status.Text += FirstString; }));
            while (true)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    //Подключение клиента
                    client.Connect(server, port);

                    //Читаем то, что есть
                    AllertListOld = ClientUtils.XMLParser("Allert.xml");
                    File.WriteAllText("Allert.xml", string.Empty); //Зачистка файла, чтобы он стал пустым для добавления в него новой инфы

                    //Создаем буферы и потоки для получения сообщения
                    byte[] NewData = new byte[1024];
                    NetworkStream stream = client.GetStream();
                    StringBuilder response = new StringBuilder();
                    int FileLengthNew = 0;
                    //Читаем сообщение из сокета, пока есть, что читать
                    do
                    {
                        FileStream FileStrNew = new FileStream("Allert.xml", FileMode.Append, FileAccess.Write);
                        int TempLength = stream.Read(NewData, 0, NewData.Length);
                        FileStrNew.Write(NewData, 0, TempLength);
                        FileLengthNew += TempLength;
                        FileStrNew.Close();
                    }
                    while (stream.DataAvailable);

                    //Забиваем в список пришедшие аллерты
                    AllertListNew = ClientUtils.XMLParser("Allert.xml");

                    //Делаем разницу B-)
                    var Difference = AllertListNew.Except(AllertListOld).ToList();

                    if (Difference.Count != 0)
                    {//Если есть различия в новых аллертах, то выводим их
                        foreach (var AllertItem in Difference)
                        {
                            var AllertPush = new ToastContentBuilder();
                            AllertPush.AddText(AllertItem.TitleName);
                            AllertPush.AddText(AllertItem.SubTitle);
                            AllertPush.AddText(AllertItem.Text);
                            AllertPush.Show();

                            string NewAllertString = "Пришел новый аллерт " + AllertItem.TitleName + " В " + DateTime.Now.ToString("h:mm:ss tt") + "\n";
                            Application.Current.Dispatcher.BeginInvoke(new Action(delegate () { Status.Text += NewAllertString; }));
                        }
                    }
                    else
                    {//Иначе говорим, что без изменений
                        //Молчу...
                    }
                    //Закрываем потоки
                    stream.Close();
                    client.Close();

                    Thread.Sleep(10000);
                }
                catch (Exception e)
                {//ДА ОН ГРЕБАННЫЙ ВОЛШЕБНИК!!!
                    Application.Current.Dispatcher.BeginInvoke(new Action(delegate () { Status.Text += e.Message + "\n"; }));
                }
            }
        }
    }
}
