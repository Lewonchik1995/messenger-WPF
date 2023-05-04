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
using System.Windows.Shapes;

namespace Messenger
{
    public partial class ClientWindow : Window
    {
        public static string ip;
        public static string name;
        private Socket server;
        public ClientWindow()
        {
            InitializeComponent();

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ip, 8888);
            SendName(name);
            ReceiveMessage();
        }

        private async Task ReceiveMessage()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                MessageListView.Items.Add(message);
            }
        }

        private async Task SendName(string name)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($"0[{name}]");
            await server.SendAsync(bytes, SocketFlags.None);
        }
        private async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($"1{message}");
            await server.SendAsync(bytes, SocketFlags.None);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage($"[{DateTime.Now}] [{name}]: {Message.Text}");
            Message.Text = "";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            server.Disconnect(true);
            this.Close();
        }
    }
}
