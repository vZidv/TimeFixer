using Microsoft.EntityFrameworkCore;
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
using TimeFixer.View.Windows;

namespace TimeFixer.View.Pages
{
    /// <summary>
    /// Interaction logic for Order_page.xaml
    /// </summary>
    public partial class Order_page : Page
    {
        public Order_page()
        {
            InitializeComponent();
        }

        private void LoadDateGrid()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                Order[] orders = db.Orders.Include(o => o.IdClientNavigation).Include(o => o.IdClockNavigation).Include(o => o.IdOrderStatusNavigation).ToArray();
                orders_dg.ItemsSource = orders;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();
            allOrders_tblock.Text = $"Всего - {orders_dg.Items.Count}";
        }

        private void orderAdd_but_Click(object sender, RoutedEventArgs e)
        {
            OrderAdd_win win = new OrderAdd_win();
            win.ShowDialog();
            LoadDateGrid();
        }

        private void search_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                Order[] clients = db.Orders.Where(o =>
                EF.Functions.Like(o.Problem, $"%{search_tb.Text}%") ||
                EF.Functions.Like(o.IdClientNavigation.LastName, $"%{search_tb.Text}%") ||
                EF.Functions.Like(o.IdClockNavigation.Model, $"%{search_tb.Text}%")).Include(o => o.IdClientNavigation).Include(o => o.IdClockNavigation).Include(o => o.IdOrderStatusNavigation).ToArray();
                orders_dg.ItemsSource = clients;

                allOrders_tblock.Text = $"Всего - {orders_dg.Items.Count}";
            }
        }
    }
}
