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
    /// Interaction logic for OrderAdd_page.xaml
    /// </summary>
    public partial class OrderAdd_page : Page, Classes.IOreder
    {
        public Client? client { get; set; }   
        public ModelClock? clock { get; set; }

        public OrderAdd_page()
        {
            InitializeComponent();
        }
        private void ClientLoad()
        {
            if (client == null)
                return;

            name_tb.Text = client.Name;
            lastName_tb.Text = client.LastName;
            patronymic_tb.Text = client.Patronymic;
        }
        private void ClockLoad()
        {
            if (clock == null)
                return;

            clockModel_tb.Text = clock.Model;
        }
        private void StatusLoad()
        {
            using(TimeFixerContext db = new TimeFixerContext())
            {
                status_cb.ItemsSource = db.OrderStatuses.ToList();
                status_cb.DisplayMemberPath = "NameStatus";
            }
        }

        private void selectClient_but_Click(object sender, RoutedEventArgs e) => Classes.Settings.frame.Navigate(new SelectClient_page(this));


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientLoad();
            ClockLoad();
            StatusLoad();
        }

        private void selectClock_but_Click(object sender, RoutedEventArgs e) => Classes.Settings.frame.Navigate(new selectClockModel_page(this));



        private void status_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addOrder_but_Click(object sender, RoutedEventArgs e)
        {
            if (client == null || clock == null || dateReceiving_dp.SelectedDate == null || dateReturn_dp.SelectedDate == null || string.IsNullOrEmpty(problem_tb.Text))
            {
                MyMessageBox.Show("Ошибка", "Пожалуйста, заполните все поля перед добавлением заказа.");
            }
            else
            {
                Order order = new Order()
                {
                    IdClient = client.Id,
                    IdClock = clock.Id,
                    DateReceiving = dateReceiving_dp.SelectedDate.Value,
                    DateReturn = dateReturn_dp.SelectedDate.Value,
                    Problem = problem_tb.Text
                };

                if (status_cb.SelectedItem is OrderStatus f)
                {
                    order.IdOrderStatus = f.Id;
                }

                using (TimeFixerContext db = new TimeFixerContext())
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                }

                MyMessageBox.Show("Внимание", "Заказ добавлен!");
                Settings.window.Close();
            }
        }
    }
}
