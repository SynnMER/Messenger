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
using Microsoft.SqlServer.Server;
using System.Windows.Media;

namespace Messenger
{
    internal class verification
    {
        private SqlConnection sqlConnection = null;
        private Registration registration = null;
        public bool check = false;
        public static string ip = "";
        public static string port = "";

        public void sqlConnect()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["messengerDB"].ConnectionString);
            sqlConnection.Open();
        }
        //посмотреть как закрывать поток 
        public void remember(string login,string password)
        {
            Thread thread = new Thread(() =>
            {
                rememberThread(login, password);
            });
            thread.Start();
        }
        public async void rememberThread(string login, string password)
        {
            bool flag = false;

            string host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(host);

            IPAddress ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand($"SELECT login, password FROM signUp", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        if (reader.GetValue(0).ToString() == login && reader.GetValue(1).ToString() == password)
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
                            ip = reader.GetValue(2).ToString();
                            port = reader.GetValue(3).ToString();
                            break;
                        }
                    }
                }
                reader.Close();
            });

            //перевод в мессенджер, где сохранены наши чаты
            if (check == true)
            {
                chat chat = new chat();
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
