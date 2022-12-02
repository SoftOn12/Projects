using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;

namespace BankAllert
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BackgroundWorker backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();

            ConsoleWPF.Text += "Старт сервера\n";

            IniManager MyIni = new IniManager("config.ini");

            Server.Port = Convert.ToInt32(MyIni.Read("port"));
            string ipString = ServerUtils.GetIp();

            ConsoleWPF.Text += "IP Сервера: " + ipString + "\n" + "Порт: " + Server.Port.ToString() + "\n";
            try
            {
                Server.AllertView(AllertList);

                backgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

                //Event creation.    
                //For the performing operation in the background.    
                backgroundWorker.DoWork += backgroundWorker_DoWork;
                backgroundWorker.RunWorkerAsync();
                if (backgroundWorker.IsBusy)
                {
                    backgroundWorker.CancelAsync();
                }
            }
            catch(Exception e)
            {
                ConsoleWPF.Text += e.Message;
            }
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Server.ServerStart(ConsoleWPF);
        }

        private void AllertInfoButton_Click(object sender, RoutedEventArgs e)
        {
            AllertInfo.Items.Clear();
            foreach (Allert item in Server.AllertList)
            {
                if(item.TitleName == AllertList.SelectedItem.ToString())
                {
                    AllertInfo.Items.Add(item.TitleName);
                    AllertInfo.Items.Add(item.SubTitle);
                    AllertInfo.Items.Add(item.Text);
                    AllertInfo.Items.Add(item.SubText);
                    AllertInfo.Items.Add(item.Time);
                }
            }
        }

        private void AllertAdd_Click(object sender, RoutedEventArgs e)
        {
            Allert newAllert = new Allert(AllertNameTextbox.Text,
                                          AllertTitleTextbox.Text,
                                          AllertTextTextbox.Text,
                                          AllertSubTextTextbox.Text,
                                          AllertTimeTextbox.Text);
            newAllert.AddToXML(Server.FileName);
            Server.AllertView(AllertList);
        }

        private void AllertRemove_Click(object sender, RoutedEventArgs e)
        {
            Allert objAllert = new Allert(AllertList.SelectedItem.ToString());
            objAllert.RemoveFromXML(Server.FileName);
            Server.AllertView(AllertList);
        }

        private void RefreshAllerList_Click(object sender, RoutedEventArgs e)
        {
            Server.AllertView(AllertList);
        }
    }
}
