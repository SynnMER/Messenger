using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using Messenger;
using static System.Net.Mime.MediaTypeNames;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    /// 
    //СДЕЛАТЬ ПОДСКАЗКУ ПРИ ВВОДЕ ЛОГИНА И ПАРОЛЯ
    public partial class Registration : Window
    {
        private verification verification = new verification();
        private string login = "Enter login";
        private string password = "Enter password";
        private bool flagLogin = false;
        private bool flagPassword = false;
        public Registration()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();
            init();
        }
        public void init()
        {
            SignIn.Click += ButtonClickSignIn;
            SignUp.Click += ButtonClickSignUp;
        }
        private void ButtonClickSignIn(object sender, RoutedEventArgs e)
        {
            //сделать замену ввода пароля на *
            if (LoginTextBox.Text == "")
                MessageBox.Show("Поле логин не может быть пустым");
            else if (PasswordTextBox.Text.Length < 8)
            {
                MessageBox.Show("Пароль должен иметь больше 8 символов");
            }
            else
            {
                verification.checkRemember(LoginTextBox.Text, PasswordTextBox.Text);
                this.Visibility = Visibility.Hidden;
            }
                
        }

        private void ButtonClickSignUp(object sender, RoutedEventArgs e)
        {
            if(LoginTextBox.Text == "")
                MessageBox.Show("Поле логин не может быть пустым");
            else if (PasswordTextBox.Text.Length < 8)
            {
                MessageBox.Show("Пароль должен иметь больше 8 символов");
            }
            else
                verification.rememberThread(LoginTextBox.Text,PasswordTextBox.Text);
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Text = login;
            LoginTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            PasswordTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            PasswordTextBox.Text = password;
        }

        private void LoginTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if(LoginTextBox.Text == login)
            {
                LoginTextBox.Text = "";
                LoginTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void LoginTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (LoginTextBox.Text == "" && flagLogin!= true)//&& LoginTextBox.CaretIndex != 0
            {
                LoginTextBox.Text = login;
                LoginTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
        private void PasswordTextBox_MouseEnter(object sender, MouseEventArgs e)
        {  
            if (PasswordTextBox.Text == password)
            {
                PasswordTextBox.Text = "";
                PasswordTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void PasswordTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
           
            if (PasswordTextBox.Text == "" && flagPassword != true)// && PasswordTextBox.CaretIndex != 0
            {
                PasswordTextBox.Text = password;
                PasswordTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void LoginTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            flagLogin = true;
            flagPassword = false;
        }

        private void PasswordTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            flagPassword = true;
            flagLogin = false;
        }
    }
}
