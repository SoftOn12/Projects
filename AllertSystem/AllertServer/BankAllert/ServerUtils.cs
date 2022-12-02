using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace BankAllert
{
    class ServerUtils
    {
        public static string GetIp()
        {
            string ip_addr;
            string host_name = Dns.GetHostName();
            IPHostEntry host_ip = Dns.GetHostByName(host_name);
            ip_addr = host_ip.AddressList[0].ToString();
            return ip_addr;
        }

        public static List<TcpClient> ClearTcpList (List<TcpClient> clientList)
        {
            List<TcpClient> temp = new List<TcpClient>();
            foreach (TcpClient item in clientList)
            {
                if (item.Client.Connected)
                {
                    temp.Add(item);
                }
            }

            foreach (var item in temp)
            {
                clientList.Remove(item);
            }
            return temp;
        }

        public static List<Allert> XMLParser(string PathToXML)
        {
            List<Allert> AllertList = new List<Allert>();
            XmlDocument xDoc = new XmlDocument();

            if (!File.Exists(PathToXML))
            {
                var xmlWriter = new XmlTextWriter(PathToXML, null);
                xmlWriter.WriteStartDocument();                  // <?xml version="1.0"?>
                xmlWriter.WriteStartElement("Data");             // <Data> 
                xmlWriter.WriteEndElement();                     // </Data>
                xmlWriter.Close();

                Allert newAllert = new Allert("Первый тестовый аллерт",
                                              "№00000",
                                              "Текст",
                                              "Комментарий",
                                              "00:00");
                newAllert.AddToXML(PathToXML);
            }

            xDoc.Load(PathToXML);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xNode in xRoot)
                {
                    // получаем атрибут name
                    Allert AllertObj = new Allert();
                    XmlNode attr = xNode.Attributes.GetNamedItem("Name");
                    AllertObj.TitleName = attr.Value;

                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xNode.ChildNodes)
                    {
                        // если узел - company
                        if (childnode.Name == "SubTitle")
                        {
                            AllertObj.SubTitle = childnode.InnerText;
                        }
                        if (childnode.Name == "Text")
                        {
                            AllertObj.Text = childnode.InnerText;
                        }
                        if (childnode.Name == "SubText")
                        {
                            AllertObj.SubText = childnode.InnerText;
                        }
                        if (childnode.Name == "Time")
                        {
                            AllertObj.Time = childnode.InnerText;
                        }
                    }
                    AllertList.Add(AllertObj);
                }
            }
            return AllertList;
        }
            
    }
}
