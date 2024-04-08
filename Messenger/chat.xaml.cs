using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        private connection connection = new connection();//Error
        public string _login;
        public bool check = false;
        public chat()
        {
            InitializeComponent();
            connection.onload();
            //this.DataContext = this;
            listItems.DataContext = new ChatViewModel();
            buttonsCont.DataContext = new createButton();
            listItems.DataContext = new ChatForServer();
        }
        public void newMessage(string text)
        {
            ((ChatForServer)listItems.DataContext).AddMessage(text);
        }
        //нажатие на кнопку  и оптравки нашего сообщения в stackPanel, scrolviewer
        private void sendMessege_Click(object sender, RoutedEventArgs e)
        {
            if (connection.flag == true)
            {
                if (enterMessege.Text != "")
                {
                    connection.sendMessage(enterMessege.Text);
                    ((ChatViewModel)listItems.DataContext).AddMessage(enterMessege.Text);
                    enterMessege.Text = "";

                    bool flag = false;
                    
                    if (namePerson.Text != "" && ((ChatViewModel)listItems.DataContext).returnCount() > 0)
                    {
                        Button[] buttons = { };
                        for (int i = 0; i < buttonsCont.Items.Count - 1; i++)
                        {
                            buttons[i] = (Button)buttonsCont.Items[i];
                            if (buttons[i].Content.ToString() == namePerson.Text)
                            {
                                flag = true;
                            }
                        }
                        if (flag == false)
                            ((createButton)buttonsCont.DataContext).AddButton(namePerson.Text);
                    }
                }
            }
        } 


        private void enterMessege_KeyDown(object sender, KeyEventArgs e)
        {
            if (connection.flag == true)
            {
                searchFriends.Text = "";
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Enter)
                {
                    if (enterMessege.Height < 105)
                        enterMessege.Height += 35;
                    enterMessege.AppendText(Environment.NewLine);
                    enterMessege.SelectionStart = enterMessege.Text.Length;
                    enterMessege.ScrollToEnd();
                }
                else if (e.Key == Key.Enter && enterMessege.Text != "")
                {
                    connection.sendMessage(enterMessege.Text);
                    ((ChatViewModel)listItems.DataContext).AddMessage(enterMessege.Text);//сделать сохранение сообщений
                    enterMessege.Text = "";

                    bool flag = false;
                    int countMessages = ((ChatViewModel)listItems.DataContext).returnCount();
                    int countButtons = ((createButton)buttonsCont.DataContext).returnCount();
                    ObservableCollection<Button> buttons = new ObservableCollection<Button>();
                    buttons = ((createButton)buttonsCont.DataContext).Buttons;
                    if (namePerson.Text != "" && countMessages > 0)
                    {
                        for (int i = 0; i < countButtons; i++)
                        {
                            if (buttons[i].Content.ToString() == namePerson.Text)
                                flag = true;
                        }
                        if (flag == false)
                            ((createButton)buttonsCont.DataContext).AddButton(namePerson.Text);
                    }
                }
            }
        }
        private void searchFriends_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                connection.searchFriends(searchFriends.Text);
                
                if (connection.flag == true && _login != searchFriends.Text)
                {
                    namePerson.Text = searchFriends.Text;
                    searchFriends.Text = "";
                    check = true;
                } 
            }
        }
    }
}
