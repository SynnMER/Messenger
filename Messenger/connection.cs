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
using System.IO;
using System.Runtime.Remoting.Messaging;


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
        public bool flag = false;
        public string login;
        //логика отправки сообщений
        public async void sendMessage(string text)
        {
            verification verification = new verification();
            StreamWriter Writer = null;
            try
            {
                Writer = new StreamWriter(verification.client.GetStream());

                if (Writer is null) return;
                // запускаем ввод сообщений
                await SendMessageAsync(Writer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Writer?.Close();

            // отправка сообщений
            async Task SendMessageAsync(StreamWriter writer)
            {
                await writer.WriteLineAsync(text);
                await writer.FlushAsync();
            }
        }

        public async void searchFriends(string login)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();

            string host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(host);
            IPAddress ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            SqlCommand changeIP = new SqlCommand($"UPDATE [signUp] SET ip='{ipv4Address}' WHERE login='{login}'", sqlConnection);
            changeIP.ExecuteNonQuery();
            sqlConnection.Close();

            StreamWriter Writer = null;
            try
            {
                Writer = new StreamWriter(verification.client.GetStream());
                if (Writer is null) return;
                // запускаем ввод сообщений
                await SendMessageAsync(Writer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Writer?.Close();

            // отправка сообщений
            async Task SendMessageAsync(StreamWriter writer)
            {
                await writer.WriteLineAsync(login);
                await writer.FlushAsync();
            }
        }
    }
}
