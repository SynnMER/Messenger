using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private verification verification = new verification();
        public Registration()
        {
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
            if (PasswordTextBox.Text.Length < 8)
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
            if (PasswordTextBox.Text.Length < 8)
            {
                MessageBox.Show("Пароль должен иметь больше 8 символов");
            }
            else
                verification.rememberThread(LoginTextBox.Text,PasswordTextBox.Text);
        }
    }
}
