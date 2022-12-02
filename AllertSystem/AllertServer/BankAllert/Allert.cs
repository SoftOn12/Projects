using System;
using System.Xml;
using System.Windows.Controls;

namespace BankAllert
{
    class Allert : IEquatable<Allert>
    {
        public string TitleName { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }
        public string SubText { get; set; }
        public string Time { get; set; }

        public bool Equals(Allert other)
        {
            if (other is null)
                return false;

            return this.TitleName == other.TitleName &&
                   this.SubTitle == other.SubTitle &&
                   this.Text == other.Text &&
                   this.SubText == other.SubText &&
                   this.Time == other.Time;
        }

        public override bool Equals(object obj) => Equals(obj as Allert);
        public override int GetHashCode() => (TitleName, SubTitle, Text, SubText, Time).GetHashCode();

        public Allert(string titleName, string subTitle="SubTitle", string text="Text", string subText="Subtext", string time="Time")
        {
            TitleName = titleName;
            SubTitle = subTitle;
            Text = text;
            SubText = subText;
            Time = time;
        }
        public Allert() { }

        public void AddToXML(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;

            // создаем новый элемент person
            XmlElement AllertElem = xDoc.CreateElement("Allert");

            // создаем атрибут name
            XmlAttribute NameAttr = xDoc.CreateAttribute("Name");

            // создаем элементы company и age
            XmlElement SubTitleElem = xDoc.CreateElement("SubTitle");
            XmlElement TextElem = xDoc.CreateElement("Text");
            XmlElement SubTextElem = xDoc.CreateElement("SubText");
            XmlElement TimeElem = xDoc.CreateElement("Time");

            // создаем текстовые значения для элементов и атрибута
            XmlText NameText = xDoc.CreateTextNode(this.TitleName);
            XmlText SubTitleText = xDoc.CreateTextNode(this.SubTitle);
            XmlText TextText = xDoc.CreateTextNode(this.Text);
            XmlText SubTextText = xDoc.CreateTextNode(this.SubText);
            XmlText TimeText = xDoc.CreateTextNode(this.Time);

            //добавляем узлы
            NameAttr.AppendChild(NameText);
            SubTitleElem.AppendChild(SubTitleText);
            TextElem.AppendChild(TextText);
            SubTextElem.AppendChild(SubTextText);
            TimeElem.AppendChild(TimeText);

            // добавляем атрибут name
            AllertElem.Attributes.Append(NameAttr);
            // добавляем элементы company и age
            AllertElem.AppendChild(SubTitleElem);
            AllertElem.AppendChild(TextElem);
            AllertElem.AppendChild(SubTextElem);
            AllertElem.AppendChild(TimeElem);
            // добавляем в корневой элемент новый элемент person
            xRoot?.AppendChild(AllertElem);
            // сохраняем изменения xml-документа в файл
            xDoc.Save(path);
        }

        public void RemoveFromXML(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlElement xNode in xRoot)
            {
                if(xNode.Attributes.GetNamedItem("Name").Value == this.TitleName)
                {
                    xRoot.RemoveChild(xNode);
                }
            }
            xDoc.Save(path);
        }

        public void AllertToConsole()
        {
            Console.WriteLine("********************************************************");
            Console.WriteLine(TitleName);
            Console.WriteLine(SubTitle);
            Console.WriteLine(Text);
            Console.WriteLine(SubText);
            Console.WriteLine("Ожидаемое время устранения неполадки: {0}", Time);
            Console.WriteLine("********************************************************");
            Console.WriteLine();
        }

        public void AllertToConsoleWPF(TextBox ConsoleWPF)
        {
            ConsoleWPF.Text += ("********************************************************" + "\n");
            ConsoleWPF.Text += (TitleName + "\n");
            ConsoleWPF.Text += (SubTitle + "\n");
            ConsoleWPF.Text += (Text + "\n");
            ConsoleWPF.Text += (SubText + "\n");
            ConsoleWPF.Text += ("Ожидаемое время устранения неполадки:" + Time + "\n");
            ConsoleWPF.Text += ("********************************************************" + "\n");
        }
    }
}
