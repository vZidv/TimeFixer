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
    /// Interaction logic for ClientEdit_page.xaml
    /// </summary>
    public partial class ClientEdit_page : Page
    {
        Client client;
        public ClientEdit_page(Client client)
        {
            InitializeComponent();
            this.client = client;

        }
        private void LoadClientData()
        {
            name_tb.Text = client.Name;
            lastName_tb.Text = client.LastName;
            patronymic_tb.Text = client.Patronymic;

            address_tb.Text = client.Address;
            phone_tb.Text = client.PhoneNumber;
            email_tb.Text = client.Email;

            LoadCombobox();

            if (client.IdHowDidFindUsNavigation != null)
                howDidfindUs_cb.SelectedIndex = client.IdHowDidFindUsNavigation.Id - 1;
        }
        private void LoadCombobox()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                howDidfindUs_cb.ItemsSource = db.HowDidFindUs.ToList();
                howDidfindUs_cb.DisplayMemberPath = "Name";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadClientData();
            LoadCombobox();
        }

        private void editClient_but_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name_tb.Text) || string.IsNullOrEmpty(lastName_tb.Text) || string.IsNullOrEmpty(address_tb.Text) || string.IsNullOrEmpty(phone_tb.Text) || string.IsNullOrEmpty(email_tb.Text) || howDidfindUs_cb.SelectedIndex == -1)
            {
                MyMessageBox.Show("Ошибка", "Пожалуйста, заполните все обязательные поля перед обновлением данных клиента.");
            }
            else
            {
                using (TimeFixerContext db = new TimeFixerContext())
                {
                    client.Name = name_tb.Text;
                    client.LastName = lastName_tb.Text;
                    client.Patronymic = patronymic_tb.Text;

                    client.Address = address_tb.Text;
                    client.PhoneNumber = phone_tb.Text;
                    client.Email = email_tb.Text;

                    if (howDidfindUs_cb.SelectedIndex != -1)
                    {
                        HowDidFindU howDidFindUs = db.HowDidFindUs.Where(o => o.Name == howDidfindUs_cb.Text).First();
                    }

                    db.Clients.Update(client);
                    db.SaveChanges();
                }
                MyMessageBox.Show("Внимание", "Данные успешно обновлены!");
                Classes.Settings.window.Close();
            }
        }
    }
}
