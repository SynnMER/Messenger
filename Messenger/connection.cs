using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Configuration;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Messenger
{
    internal class connection
    {
        private SqlConnection sqlConnection = null;
        public static bool flag = false;
        //логика отправки сообщений
        public void sendMessage(string text)
        {

        }
        public async void searchFriends(string login)
        {
            //поиск пользователя по логину
            string adresIP = "";
            string port = "8080";
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();

            //=============== сделать подсказку
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand($"SELECT login, password, ip, port FROM signUp", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        if (reader.GetValue(0).ToString() == login)
                        {
                            adresIP = reader.GetValue(2).ToString();
                            port = reader.GetValue(3).ToString();
                            break;
                        }
                            
                    }
                }
                reader.Close();
            });
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(adresIP), int.Parse(port));
                socket.Connect(ipEndPoint);
                if (socket.Connected)
                    flag = true;
            }
            catch (Exception)
            { }
            //================ отkрытие чата с этим человеком


        }
        public async void onload()
        {
            verification verification = new verification();
            TcpListener serverListener = new TcpListener(IPAddress.Parse(verification.ip), int.Parse(verification.port));
            serverListener.Start();
            var tcpClient = await serverListener.AcceptTcpClientAsync();
            //============== продолжение логики с подключением 
            if (tcpClient.Connected)
            {

            }
            //================== сделать закрытие подключения при закрытии приложения
        }
    }
}
