using Microsoft.Toolkit.Uwp.Notifications;
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
using System.ComponentModel;

namespace ClientAllert
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
            Status.Text = "Подключение к серверу...\n";
            Client.AllertView(AllertList);
            try
            {
                backgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };  
                backgroundWorker.DoWork += backgroundWorker_DoWork;
                backgroundWorker.RunWorkerAsync();
                if (backgroundWorker.IsBusy)
                {
                    backgroundWorker.CancelAsync();
                }
            }
            catch (Exception e)
            {
                Status.Text = e.Message + "\n";
            }
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Client.ClientStart(Status);
        }

        private void AllertInfoButton_Click(object sender, RoutedEventArgs e)
        {
            AllertInfo.Items.Clear();
            foreach (Allert item in Client.AllertList)
            {
                if (item.TitleName == AllertList.SelectedItem.ToString())
                {
                    AllertInfo.Items.Add(item.TitleName);
                    AllertInfo.Items.Add(item.SubTitle);
                    AllertInfo.Items.Add(item.Text);
                    AllertInfo.Items.Add(item.SubText);
                    AllertInfo.Items.Add(item.Time);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Client.AllertView(AllertList);
        }
    }
}
