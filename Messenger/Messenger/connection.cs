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
        public bool flag = false;
        private IPAddress ipv4Address;
        //логика отправки сообщений
        public void sendMessage(string text)
        {

        }
        public void searchFriends(string login)
        {
            //поиск пользователя по логину
            string adresIP = "";
            string port = "8080";
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();
            //но только если в сети
            SqlCommand changeIP = new SqlCommand($"UPDATE [signUp] SET ip='{ipv4Address}' WHERE login='{login}'", sqlConnection);
            changeIP.ExecuteNonQuery();

            //=============== сделать подсказку
            //здесь нужно сделать поток, но позже, когда будет добавлено много пользователей
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
            //================ отkрытие чата с этим человеком
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


        }

        public async void onload()
        {

            string host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(host);
            ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            TcpListener serverListener = new TcpListener(ipv4Address, 8080);
            //error, ошибка подключения
            serverListener.Start();
            var tcpClient = await serverListener.AcceptTcpClientAsync();
            //============== продолжение логики с подключением 
            if (tcpClient.Connected)//если подключился
            {

            }
            //================== сделать закрытие подключения при закрытии приложения
        }
    }
}
