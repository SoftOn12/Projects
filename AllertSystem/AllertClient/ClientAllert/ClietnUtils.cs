using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClientAllert
{
    class ClientUtils
    {
        public static string GetIp()
        {
            string ip_addr;
            string host_name = Dns.GetHostName();
            IPHostEntry host_ip = Dns.GetHostByName(host_name);
            ip_addr = host_ip.AddressList[0].ToString();
            return ip_addr;
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

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);
        static public bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.  
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }

        static public bool AllertListCompare(List<Allert> list1, List<Allert> list2)
        {
            var firstNotSecond = list1.Except(list2).ToList();
            var secondNotFirst = list2.Except(list1).ToList();
            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }

        static public bool AllertCompare(Allert First, Allert Second)
        {
            if (First.TitleName == Second.TitleName &&
               First.SubTitle == Second.SubTitle &&
               First.Text == Second.Text &&
               First.SubText == Second.SubText &&
               First.Time == Second.Time)
                return true;

            else return false;
        }
    }
}
