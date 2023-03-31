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
using System.Windows.Shapes;
using TimeFixer.Classes;

namespace TimeFixer.View.Windows
{
    /// <summary>
    /// Interaction logic for ClientAdd_win.xaml
    /// </summary>
    public partial class ClientAdd_win : Window
    {
        public ClientAdd_win()
        {
            InitializeComponent();
        }

        private void addClient_but_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name_tb.Text) || string.IsNullOrEmpty(lastName_tb.Text) || string.IsNullOrEmpty(patronymic_tb.Text) || string.IsNullOrEmpty(phone_tb.Text) || string.IsNullOrEmpty(email_tb.Text) || string.IsNullOrEmpty(address_tb.Text))
            {
                MyMessageBox.Show("Ошибка", "Пожалуйста, заполните все поля перед добавлением пользователя.");
            }
            else
            {
                Client client = new Client()
                {
                    Name = name_tb.Text,
                    LastName = lastName_tb.Text,
                    Patronymic = patronymic_tb.Text,

                    PhoneNumber = phone_tb.Text,
                    Email = email_tb.Text,

                    Address = address_tb.Text
                };

                if (howDidfindUs_cb.SelectedItem is HowDidFindU f)
                {
                    client.IdHowDidFindUs = f.Id;
                }

                using (TimeFixerContext db = new TimeFixerContext())
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                }

                MyMessageBox.Show("Внимание", "Пользователь добавлен!", MyMessageBoxOptions.Ok);
                this.Close();
            }
        }

        private void LoadCombobox()
        {
            using(TimeFixerContext db = new TimeFixerContext())
            {
                howDidfindUs_cb.ItemsSource = db.HowDidFindUs.ToList();
                howDidfindUs_cb.DisplayMemberPath = "Name";
            }    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCombobox();
        }

        private void howDidfindUs_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
