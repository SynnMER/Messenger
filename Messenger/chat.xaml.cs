using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для chat.xaml
    /// </summary>
    public partial class chat : Window
    {
        public string _login;
        public bool check = false;
        public bool secur = false;
        public string log;
        private bool enterMessege_Key = false;
        public chat()
        {
            InitializeComponent();
            listItems.DataContext = new ChatViewModel();
            buttonsCont.DataContext = new createButton();
            //listItems.DataContext = new ChatForServer();
            //Server();
            onload();
        }
        public async void onload()
        {
            StreamReader Reader = null;
            try
            {
                Reader = new StreamReader(verification.client.GetStream());
                if (Reader is null) return;
                // запускаем новый поток для получения данных
                await Dispatcher.Invoke(() => ReceiveMessageAsync(Reader));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Reader?.Close();
            // получение сообщений
            async Task ReceiveMessageAsync(StreamReader reader)
            {
                // считываем ответ в виде строки
                while (enterMessege_Key == false)//true
                {
                    string message = await reader.ReadLineAsync();
                    if (namePerson.Text == "")
                    {
                        print(message);
                    }
                    else
                    {
                        printMessage(message);//вывод сообщения
                    }
                }
                
            }
            
        }
        public void print(string message)
        {
            if (_login != searchFriends.Text)
                namePerson.Text = message;
        }
        public void printMessage(string text)
        {
            ((ChatViewModel)listItems.DataContext).AddMessage(text);
        }

        //нажатие на кнопку  и оптравки нашего сообщения в stackPanel, scrolviewer
        private void sendMessege_Click(object sender, RoutedEventArgs e)
        {
            connection connection = new connection();
            if (verification.flag == true)
            {
                if (enterMessege.Text != "")
                {
                    enterMessege_Key = true;
                    connection.sendMessage(enterMessege.Text);
                    ((ChatViewModel)listItems.DataContext).AddMessage(enterMessege.Text);
                    enterMessege.Text = "";

                    bool flag = false;
                    secur = true;
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
                chat chat = new chat();
            }
            secur = false;
        } 


        private void enterMessege_KeyDown(object sender, KeyEventArgs e)
        {
            connection connection = new connection();
            if (verification.flag == true)
            {
                enterMessege_Key = true;
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
                    secur = true;
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
                    secur = false;
                    chat chat = new chat();
                }
                
            }
        }
        private void searchFriends_KeyDown(object sender, KeyEventArgs e)
        {
            connection connection = new connection();
            if (e.Key == Key.Enter)
            {
                connection.searchFriends(searchFriends.Text);
                print(searchFriends.Text);
                //namePerson.Text = searchFriends.Text;
                if (verification.flag == true && _login != searchFriends.Text)
                {
                    searchFriends.Text = "";
                    check = true;
                } 
            }
        }
        
    }
}
