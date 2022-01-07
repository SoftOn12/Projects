using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;


namespace SocketStudy_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 8005);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Bind(ipep);
                socket.Listen(10);
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while(true)
                {
                    Socket client = socket.Accept();
                    //StringBuilder builder = new StringBuilder();
                    //byte[] TextData = new byte[8192]; // буфер для текста

                    do
                    {
                        MemoryStream MemStr = new MemoryStream();
                        FileStream FileLog = File.Create("logger.log");

                        while (true)
                        {
                            byte[] buffer = new byte[4096];
                            int FileBytes = client.Receive(buffer);
                            if (FileBytes == 0)
                            {
                                break;
                            }
                            MemStr.Write(buffer, 0, buffer.Length);
                        }
                        MemStr.WriteTo(FileLog);
                        FileLog.Close();
                        MemStr.Close();
                    }
                    while (client.Available > 0);
                    Console.WriteLine("Файл получен");

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
            catch(Exception exe)
            {
                Console.WriteLine(exe.Message);
            }
        }
    }
}
