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
    /// Interaction logic for UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        public UserAdd()
        {
            InitializeComponent();
        }

        private void addUser_but_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(login_tb.Text) || string.IsNullOrEmpty(password_tb.Text) || string.IsNullOrEmpty(status_cb.Text))
            {
                MyMessageBox.Show("Ошибка", "Пожалуйста, заполните все поля перед добавлением пользователя.");
            }
            else
            {
                UserSetting setting = new UserSetting();
                using (TimeFixerContext db = new TimeFixerContext())
                {
                    setting.IdStatus = db.UserStatuses.Where(o => o.StatusName == status_cb.Text).FirstOrDefault().Id;
                    db.UserSettings.Add(setting);
                    db.SaveChanges();
                    User user = new User()
                    {
                        Login = login_tb.Text,
                        Password = password_tb.Text,
                        IdSetting = setting.Id
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                MyMessageBox.Show("Внимание", "Пользователь добавлен!", MyMessageBoxOptions.Ok);
                this.Close();
            }
        }
        private void LoadCombobox()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                status_cb.ItemsSource = db.UserStatuses.ToList();
                status_cb.DisplayMemberPath = "StatusName";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCombobox();
        }
    }
}
