﻿using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Registration registration;
        public MainWindow()
        {
            InitializeComponent();
            init();
        }
        public void init()
        {
            buttonFirst.Click += ButtonClick;
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            registration = new Registration();
            registration.Show();
        }


    }
}
