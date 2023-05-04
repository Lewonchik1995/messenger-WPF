using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
    public partial class ServerWindow : Window
    {
        private Socket socket;
        public static string name;
        private Dictionary<Socket, string> clients = new Dictionary<Socket, string>();
        public static List<string> logList = new List<string>();
        private bool isPageOpen = false;
        public ServerWindow()
        {
            InitializeComponent();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(1000);
            clients.Add(socket, $"[{name}]");
            logList.Add($"[{DateTime.Now}] \nНовый юзер: {name} ");
            UpdateUsers();
            ListenToClients();
        }
        private async Task ListenToClients()
        {
            while (true)
            {
                var client = await socket.AcceptAsync();
                ReceiveMessage(client);
            }
        }

        private async Task ReceiveMessage(Socket client)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                int action = Convert.ToInt32(message.Substring(0, 1));
                message = message.Substring(1, message.Length - 1);
                switch (action)
                {
                    case 0:
                        message = message.Substring(0, message.LastIndexOf(']')+1);
                        clients.Add(client, message);
                        UpdateUsers();
                        logList.Add($"[{DateTime.Now}] \nНовый юзер: {message} ");
                        string allUsers = "";
                        foreach (var item in clients)
                        {
                            allUsers += $"{item.Value};";
                        }
                        foreach (var item in clients)
                        {
                            SendUsers(item.Key, allUsers);
                        }

                        break;
                    case 1:
                        MessageListView.Items.Add(message);

                        foreach (var item in clients)
                        {
                            SendMessage(item.Key, message);
                        }
                        break;
                }
            }
        }

        private async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($"1{message}");
            await client.SendAsync(bytes, SocketFlags.None);
        }

        private async Task SendUsers(Socket client, string allUsers)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($"2{allUsers}");
            await client.SendAsync(bytes, SocketFlags.None);
        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            if (!isPageOpen)
            {
                Frame.Content = new ServerPageLogs();
                isPageOpen = true;
                ShowLogs.Content = "Посмотреть пользователей чата";
            }
            else
            {
                Frame.Content = null;
                isPageOpen = false;
                ShowLogs.Content = "Посмотреть логи чата";
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Text != "")
            {
                MessageListView.Items.Add($"[{DateTime.Now}] [{name}]: {Message.Text}");
                foreach (var item in clients)
                {
                    SendMessage(item.Key, $"[{DateTime.Now}] [{name}]: {Message.Text}");
                }
                Message.Text = "";
            }
            else
            {
                MessageBox.Show("Введите сообщение!");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            socket.Disconnect(true);
            this.Close();
        }

        private void UpdateUsers()
        {
            UsersList.Items.Clear();
            foreach (var item in clients)
            {
                UsersList.Items.Add(item.Value);
            }
        }
    }
}