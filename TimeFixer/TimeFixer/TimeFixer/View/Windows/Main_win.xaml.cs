﻿using System;
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
using System.Windows.Shapes;
using TimeFixer.View.Pages;

namespace TimeFixer.View.Windows
{
    /// <summary>
    /// Interaction logic for Main_win.xaml
    /// </summary>
    public partial class Main_win : Window
    {
        public Main_win()
        {
            InitializeComponent();           
        }

        private void NavigationView_Navigated(Wpf.Ui.Controls.Interfaces.INavigation sender, Wpf.Ui.Common.RoutedNavigationEventArgs e)
        {

        }

        private void NavigationItem_Click(object sender, RoutedEventArgs e)
        {
            Authorization_win win = new Authorization_win();
            win.Show();
            this.Close();
        }


        private void client_but_Click(object sender, RoutedEventArgs e) => mainFrame.Navigate(new Client_page());


        private void exit_but_Click(object sender, RoutedEventArgs e)
        {
            Authorization_win win = new Authorization_win();
            win.Show();
            this.Close();
        }

        private void clock_but_Click(object sender, RoutedEventArgs e) => mainFrame.Navigate(new Clock_page());
        private void order_but_Click(object sender, RoutedEventArgs e) => mainFrame.Navigate(new Order_page());

        private void users_but_Click(object sender, RoutedEventArgs e) => mainFrame.Navigate(new User_page());


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.Settings.user.IdSettingNavigation.IdStatusNavigation.StatusName == "admin")
                users_but.Visibility = Visibility.Visible;
            else
                users_but.Visibility = Visibility.Hidden;

        }
    }
}
