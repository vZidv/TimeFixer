﻿using Microsoft.EntityFrameworkCore;
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

namespace TimeFixer.View.Pages
{
    /// <summary>
    /// Interaction logic for Client_page.xaml
    /// </summary>
    public partial class Client_page : Page
    {
        public Client_page()
        {
            InitializeComponent();
        }

        private void LoadDateGrid()
        {
            using(TimeFixerContext db = new TimeFixerContext())
            {
                Client[] clients = db.Clients.Include(o => o.IdHowDidFindUsNavigation).ToArray();
                clients_dg.ItemsSource = clients;
            }           
        }
        private int ClientCount()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                Client[] clients = db.Clients.ToArray();
                return clients.Length;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();            
            allClients_tblock.Text = $"Всего клиентов - {ClientCount()}";
        }
    }
}