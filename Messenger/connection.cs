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
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Documents;


//Задачи на 02.04
//2. Сделать добавление друзей в кнопки при открытие чата с ними
//Задачи на 03.04
//1. Добавить дату, при изменение дня , сверху посередине
//2. Подключить отправку сообщений другому человеку
//3. Сделать правильное расположение сообщений
//Задачи на 04.04 
//1. Сделать сохранение сообщений в файл
//2. Сделать сохранение наших друзей в файл
//3. Подключить переписки с людьми(у каждого человека, своя переписка, и все это сохранять)
//Задачи на 05.04
//1. Подключить отправку фото через символ на форме, другому человеку
//2. Доделать оставшиеся задачи
namespace Messenger
{
    internal class connection
    {
        private SqlConnection sqlConnection = null;
        
        public bool flag = false;
        private verification verification = new verification();
        private TcpClient tcpClient = new TcpClient();
        //логика отправки сообщений
        public async void sendMessage(string text)
        {
            var stream = tcpClient.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(text);
            // отправляем данные
            await stream.WriteAsync(data, 0, 1024);
        }
        public async void searchFriends(string login)
        {
            //поиск пользователя по логину
            string adresIP = "";
            string port = "8080";
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();

            string host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(host);
            IPAddress ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

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
                await tcpClient.ConnectAsync(IPAddress.Parse(adresIP), int.Parse(port));

                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(adresIP), int.Parse(port));
                //socket.Connect(ipEndPoint);
                if (tcpClient.Connected)
                    flag = true;
            }
            catch (Exception)
            { flag = false;
                MessageBox.Show("yes");
            }




            sqlConnection.Close();
        }

        public async void onload()
        {
            string ip = verification._ip;
            string port = verification._port;

            TcpListener serverListener = new TcpListener(IPAddress.Parse(ip), int.Parse(port));//IPAddress.Parse(ip) проблема с ip какой ip может приниматься 
            serverListener.Start();
            var tcpClient = await serverListener.AcceptTcpClientAsync();
            if (tcpClient.Connected)
            {
                try
                {
                    var stream = tcpClient.GetStream();
                    var response = new List<byte>();
                    int bytesRead = 1024;
                    // считываем данные до конечного символа
                    while ((bytesRead = stream.ReadByte()) != '\n')
                    {
                        // добавляем в буфер
                        response.Add((byte)bytesRead);
                    }
                    var word = Encoding.UTF8.GetString(response.ToArray());// наше слово полученное с клиента
                    chat chat = new chat();//error
                    chat.newMessage(word);
                    response.Clear();
                }
                finally
                {
                    serverListener.Stop();
                }
            }

        }
    }
}
