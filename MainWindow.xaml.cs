using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Xml.Linq;

namespace Messenger
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "")
            {
                TcpServer.name = Name.Text.ToString();
                ServerWindow serverWindow = new ServerWindow();
                serverWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните имя пользователя!");
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" && IP.Text != "")
            {
                TcpClient.ip = IP.Text.ToString();
                TcpClient.name = Name.Text.ToString();
                ClientWindow clientWindow = new ClientWindow();
                clientWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Имя пользователя и ip-адрес должны быть заполнены!");
            }
        }
    }
}
