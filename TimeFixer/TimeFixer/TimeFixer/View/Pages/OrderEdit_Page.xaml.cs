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
using TimeFixer.Classes;

namespace TimeFixer.View.Pages
{
    /// <summary>
    /// Interaction logic for OrderEdit_Page.xaml
    /// </summary>
    public partial class OrderEdit_Page : Page, Classes.IOreder
    {
        Order order;

        public Client? client { get; set; }
        public ModelClock? clock { get; set; }

        public OrderEdit_Page(Order order)
        {
            InitializeComponent();
            this.order = order;
        }
        public void LoadOrderData()
        {
            if (client == null)
            {
                name_tb.Text = order.IdClientNavigation.Name;
                lastName_tb.Text = order.IdClientNavigation.LastName;
                patronymic_tb.Text = order.IdClientNavigation.Patronymic;
            }
            else
            {
                name_tb.Text = client.Name;
                lastName_tb.Text = client.LastName;
                patronymic_tb.Text = client.Patronymic;
            }
            if (clock == null)
                clockModel_tb.Text = order.IdClockNavigation.Model;
            else
                clockModel_tb.Text = clock.Model;

            dateReceiving_dp.SelectedDate = order.DateReceiving;
            dateReturn_dp.SelectedDate = order.DateReturn;

            problem_tb.Text = order.Problem;

            status_cb.Text = order.IdOrderStatusNavigation.NameStatus;
        }
        private void StatusLoad()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                status_cb.ItemsSource = db.OrderStatuses.ToList();
                status_cb.DisplayMemberPath = "NameStatus";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StatusLoad();
            LoadOrderData();
        }

        private void selectClient_but_Click(object sender, RoutedEventArgs e)
        {
            Classes.Settings.frame.Navigate(new SelectClient_page(this));
            Page_Loaded(sender, e);
        }

        private void selectClock_but_Click(object sender, RoutedEventArgs e)
        {
            Classes.Settings.frame.Navigate(new selectClockModel_page(this));
            Page_Loaded(sender, e);
        }

        private void editOrder_but_Click(object sender, RoutedEventArgs e)
        {
            if (client == null || clock == null || !dateReceiving_dp.SelectedDate.HasValue || !dateReturn_dp.SelectedDate.HasValue || string.IsNullOrEmpty(problem_tb.Text) || status_cb.SelectedItem == null)
            {
                MyMessageBox.Show("Ошибка", "Пожалуйста, заполните все обязательные поля перед обновлением заказа.");
            }
            else
            {
                order.IdClient = client.Id;
                order.IdClock = clock.Id;
                order.DateReceiving = dateReceiving_dp.SelectedDate.Value;
                order.DateReturn = dateReturn_dp.SelectedDate.Value;
                order.Problem = problem_tb.Text;

                if (status_cb.SelectedItem is OrderStatus f)
                {
                    order.IdOrderStatus = f.Id;
                }
                else
                {
                    order.IdOrderStatus = 1;
                }

                using (TimeFixerContext db = new TimeFixerContext())
                {
                    db.Orders.Update(order);
                    db.SaveChanges();
                }

                MyMessageBox.Show("Внимание", "Заказ обновлен!");
                Settings.window.Close();
            }
        }

    }
}
