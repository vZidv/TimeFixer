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
    /// Interaction logic for UserEdit_page.xaml
    /// </summary>
    public partial class UserEdit_page : Page
    {
        User user;
        public UserEdit_page(User user)
        {
            InitializeComponent();
            this.user = user;
        }
        private void LoadUserData()
        {
            login_tb.Text = user.Login;
            password_tb.Text = user.Password;
            //LoadCombobox();
            //status_cb.Text = user.IdSettingNavigation.IdStatusNavigation.StatusName;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserData();
            //LoadCombobox();
        }

        private void editClient_but_Click(object sender, RoutedEventArgs e)
        {
            user.Login = login_tb.Text;
            user.Password = password_tb.Text;



            using (TimeFixerContext db = new TimeFixerContext())
            {
                //if (status_cb.SelectedIndex == -1)
                //{
                //    MyMessageBox.Show("Ошибка", "Тип пользователя не выбран!", true);
                //    return;
                //}
                //else
                //    db.UserSettings.Where(o=>o.Id == user.IdSetting).First().IdStatus = db.UserStatuses.Where(o => o.StatusName == status_cb.Text).First().Id;

                db.Users.Update(user);
                db.SaveChanges();
            }
            MyMessageBox.Show("Внимание", "Данные успешно обновлены!");
            Classes.Settings.window.Close();

        }
        //private void LoadCombobox()
        //{
        //    using (TimeFixerContext db = new TimeFixerContext())
        //    {
        //        status_cb.ItemsSource = db.UserStatuses.ToList();
        //        status_cb.DisplayMemberPath = "StatusName";
        //    }
        //}
    }
}
