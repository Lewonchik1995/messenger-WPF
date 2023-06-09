﻿using System;
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
using System.Threading;

namespace Messenger
{
    public partial class ServerWindow : Window
    {
        private CancellationTokenSource isWorking;
        private bool isPageOpen = false;
        public ServerWindow()
        {
            InitializeComponent();

            TcpServer.Server();
            isWorking = new CancellationTokenSource();
            UpdateUsers();
            ListenToClients(isWorking.Token);
        }

        private async Task ListenToClients(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var client = await TcpServer.socket.AcceptAsync();
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
                        TcpServer.clients.Add(client, message);
                        UpdateUsers();
                        TcpServer.logList.Add($"[{DateTime.Now}] \nНовый юзер: {message} ");
                        string allUsers = "";
                        foreach (var item in TcpServer.clients)
                        {
                            allUsers += $"{item.Value};";
                        }
                        foreach (var item in TcpServer.clients)
                        {
                            TcpServer.SendUsers(item.Key, allUsers);
                        }

                        break;
                    case 1:
                        MessageListView.Items.Add(message);

                        foreach (var item in TcpServer.clients)
                        {
                            TcpServer.SendMessage(item.Key, message);
                        }
                        break;
                }
            }
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
            if (Message.Text == "/disconnect")
            {
                ExitAction();
            }
            else 
            {
                if (Message.Text != "")
                {
                    MessageListView.Items.Add($"[{DateTime.Now}] [{TcpServer.name}]: {Message.Text}");
                    foreach (var item in TcpServer.clients)
                    {
                        TcpServer.SendMessage(item.Key, $"[{DateTime.Now}] [{TcpServer.name}]: {Message.Text}");
                    }
                    Message.Text = "";
                }
                else
                {
                    MessageBox.Show("Введите сообщение!");
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ExitAction();
        }

        private void UpdateUsers()
        {
            UsersList.Items.Clear();
            foreach (var item in TcpServer.clients)
            {
                UsersList.Items.Add(item.Value);
            }
        }

        private void ExitAction()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            isWorking.Cancel();
            TcpServer.socket.Close();
            TcpServer.clients = new Dictionary<Socket, string>();
            TcpServer.logList = new List<string>();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            isWorking.Cancel();
            TcpServer.socket.Close();
            TcpServer.clients = new Dictionary<Socket, string>();
            TcpServer.logList = new List<string>();
        }
    }
}