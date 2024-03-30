using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для chat.xaml
    /// </summary>
    public partial class chat : Window
    {
        private connection connection = new connection();
        //наш контейнер textBlock 
        public ObservableCollection<TextBlock> myTextBlock { get; set; } = new ObservableCollection<TextBlock>();
        private createElements createElement = new createElements();
        //private bool flag;
        public chat()
        {
            InitializeComponent();
            connection.onload();
            this.DataContext = this;
        }
        //нажатие на кнопку  и оптравки нашего сообщения в stackPanel, scrolviewer
        private void sendMessege_Click(object sender, RoutedEventArgs e)
        {
            if (connection.flag == true)
            {
                if (enterMessege.Text != "")
                {
                    connection.sendMessage(enterMessege.Text);
                    myTextBlock.Add(createElement.CreateTextBlock(enterMessege.Text));
                    enterMessege.Text = "";
                }
            }

        } 


        private void enterMessege_KeyDown(object sender, KeyEventArgs e)
        {
            if(connection.flag == true)
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Enter)
                {
                    if (enterMessege.Height < 105)
                        enterMessege.Height += 35;
                    enterMessege.AppendText(Environment.NewLine);
                    enterMessege.SelectionStart = enterMessege.Text.Length;
                    enterMessege.ScrollToEnd();
                }
                else if(e.Key == Key.Enter && enterMessege.Text != "")
                {
                    connection.sendMessage(enterMessege.Text);
                    myTextBlock.Add(createElement.CreateTextBlock(enterMessege.Text));
                    enterMessege.Text = "";
                }
            }
        }

        private void searchFriends_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                connection.searchFriends(searchFriends.Text);
                if (connection.flag == true)
                {
                    namePerson.Text = searchFriends.Text;
                    searchFriends.Text = "";
                }
                    
            }
        }
        //сделать всплывающие подсказки при изменение текста, на логины
        private void searchFriends_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
