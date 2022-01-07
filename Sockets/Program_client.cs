using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace SocketStudy_Client
{
    class Program
    {
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {

            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(ipep);
                Console.Write("Введите сообщение: ");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("Ответ сервера: " + builder.ToString());

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
