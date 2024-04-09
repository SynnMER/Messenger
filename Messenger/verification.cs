using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Media.Animation;
using System.Net.Sockets;
using System.Net;
using System.Windows.Media;
using System.Windows.Markup;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Messaging;

namespace Messenger
{
    internal class verification
    {
        private Registration registration = null;
        private connection connection = new connection();
        public static TcpClient client = new TcpClient();
        public static bool flag = false;
        public bool check = false;
        public static string _ip = "";
        public static string _port = "";

        public async void rememberThread(string login, string password)
        {
            bool flag = false;

            string host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(host);
            IPAddress ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand($"SELECT login FROM signUp", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        if (reader.GetValue(0).ToString() == login)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                reader.Close();
            });
            if (flag == false && ipv4Address != null)
            {
                SqlCommand commandInsert = new SqlCommand($"INSERT INTO [signUp] (login, password, ip, port) VALUES ('{login}', '{password}', '{ipv4Address}', '{8080}')", sqlConnection);
                commandInsert.ExecuteNonQuery();
                MessageBox.Show("Пользователь успешно зарегистрирован");
            }
            else
                MessageBox.Show("Пользователь с таким login уже зарегистрирован");
            sqlConnection.Close();
        }

        public async void checkRemember(string login, string password)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();

            string host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(host);
            IPAddress ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            SqlCommand changeIP = new SqlCommand($"UPDATE [signUp] SET ip='{ipv4Address}' WHERE login='{login}'", sqlConnection);
            changeIP.ExecuteNonQuery();
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand($"SELECT login, password, ip, port FROM signUp", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        if (reader.GetValue(0).ToString() == login && reader.GetValue(1).ToString() == password)
                        {
                            check = true;
                            _ip = reader.GetValue(2).ToString();
                            _port = reader.GetValue(3).ToString();
                            break;
                        }
                    }
                }
                reader.Close();
            });

            //перевод в мессенджер, где сохранены наши чаты
            if (check == true)
            {
                try
                {
                    client.Connect(_ip, int.Parse(_port)); //подключение клиента ip
                    if (client.Connected)
                    {
                        flag = true;
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                StreamWriter Writer = null;
                try
                {
                    Writer = new StreamWriter(client.GetStream());
                    if (Writer is null) return;
                    // запускаем ввод сообщений
                    await SendMessageAsync(Writer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // отправка сообщений
                async Task SendMessageAsync(StreamWriter writer)
                {
                    await writer.WriteLineAsync(login);
                    await writer.FlushAsync();
                }

                chat chat = new chat();
                connection.login = login;
                chat._login = login;
                chat.Show();
            }
            else
            {
                registration = new Registration();
                registration.Show();
                MessageBox.Show("Указан неправильный логин или пароль");
                
            }
            sqlConnection.Close();
        }
    }
}
