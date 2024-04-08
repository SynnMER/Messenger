using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace Messenger
{
    //класс создания элемента TextBlock
    public class ChatViewModel : INotifyPropertyChanged//для сервера
    {
        private ObservableCollection<Border> _myTextBlock;
        public ObservableCollection<Border> MyTextBlock
        {
            get { return _myTextBlock; }
            set
            {
                _myTextBlock = value;
                OnPropertyChanged("MyTextBlock");
            }
        }

        public ChatViewModel()
        {
            MyTextBlock = new ObservableCollection<Border>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddMessage(string text)
        {
            TextBlock textBlock = new TextBlock();
            Border border = new Border();
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
            TextWrapping textWrapping = TextWrapping.Wrap;
            textBlock.MinHeight = 30;
            textBlock.Background = new SolidColorBrush(Colors.White);
            textBlock.Margin = new Thickness(12, 0, 20, 0);
            textBlock.MinWidth = 20;
            textBlock.MaxWidth = 415;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.Text = text;
            textBlock.Padding = new Thickness(4);
            textBlock.HorizontalAlignment = horizontalAlignment;
            textBlock.TextWrapping = textWrapping;
            textBlock.FontSize = 18;
            textBlock.FontFamily = new FontFamily("Lucida Sans");
            textBlock.TextAlignment = TextAlignment.Left;

            DateTime date = DateTime.Now;
            string time = date.ToShortTimeString();
            Run timeRun = new Run(time)
            {
                FontFamily = new FontFamily("Lucida Sans"),
                Foreground = new SolidColorBrush(Colors.Gray),
                FontSize = 12,
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold
            };

            textBlock.Inlines.Add("  ");
            textBlock.Inlines.Add(timeRun);

            border.BorderThickness = new Thickness(1); // Установка толщины границы
            border.CornerRadius = new CornerRadius(15);
            border.Background = new SolidColorBrush(Colors.White);
            border.Child = textBlock;

            MyTextBlock.Add(border);
        }
        public int returnCount()
        {
            return MyTextBlock.Count;
        }
    }
    public class createButton : INotifyPropertyChanged
    {
        private ObservableCollection<Button> _buttons;
        private Button button = new Button();
        public ObservableCollection<Button> Buttons
        {
            get { return _buttons; }
            set
            {
                _buttons  = value;
                OnPropertyChanged("Buttons");
            }
        }

        public createButton()
        {
            Buttons = new ObservableCollection<Button>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddButton(string login)
        {
            button.MinHeight = 60;
            button.Background = new SolidColorBrush(Colors.White);
            button.Margin = new Thickness(12, 0, 20, 0);
            button.MinWidth = 20;
            button.MaxWidth = 250;
            button.Foreground = new SolidColorBrush(Colors.Black);
            button.Content = login;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            button.Padding = new Thickness(4);
            button.FontSize = 18;
            button.FontFamily = new FontFamily("Lucida Sans");

            Buttons.Add(button);
        }
        public int returnCount()
        {
            return Buttons.Count;
        }
    }
    public class ChatForServer : INotifyPropertyChanged//для клиента
    {
        private ObservableCollection<Border> _myTextBlock;
        public ObservableCollection<Border> MyTextBlock
        {
            get { return _myTextBlock; }
            set
            {
                _myTextBlock = value;
                OnPropertyChanged("MyTextBlock");
            }
        }

        public ChatForServer()
        {
            MyTextBlock = new ObservableCollection<Border>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddMessage(string text)
        {
            TextBlock textBlock = new TextBlock();
            Border border = new Border();
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
            TextWrapping textWrapping = TextWrapping.Wrap;
            textBlock.MinHeight = 30;
            textBlock.Background = new SolidColorBrush(Colors.White);
            textBlock.Margin = new Thickness(12, 0, 20, 0);
            textBlock.MinWidth = 20;
            textBlock.MaxWidth = 415;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.Text = text;
            textBlock.Padding = new Thickness(4);
            textBlock.HorizontalAlignment = horizontalAlignment;
            textBlock.TextWrapping = textWrapping;
            textBlock.FontSize = 18;
            textBlock.FontFamily = new FontFamily("Lucida Sans");
            textBlock.TextAlignment = TextAlignment.Left;

            DateTime date = DateTime.Now;
            string time = date.ToShortTimeString();
            Run timeRun = new Run(time)
            {
                FontFamily = new FontFamily("Lucida Sans"),
                Foreground = new SolidColorBrush(Colors.Gray),
                FontSize = 12,
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold
            };

            textBlock.Inlines.Add("  ");
            textBlock.Inlines.Add(timeRun);

            border.BorderThickness = new Thickness(1); // Установка толщины границы
            border.CornerRadius = new CornerRadius(15);
            border.Background = new SolidColorBrush(Colors.White);
            border.Child = textBlock;

            MyTextBlock.Add(border);
        }
    }

}
