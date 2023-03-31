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
using TimeFixer.Classes;
using TimeFixer.View.Windows;

namespace TimeFixer.View.Pages
{
    /// <summary>
    /// Interaction logic for User_page.xaml
    /// </summary>
    public partial class User_page : Page
    {
        public User_page()
        {
            InitializeComponent();
        }
        private void LoadDateGrid()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                User[] clients = db.Users.Include(o => o.IdSettingNavigation.IdStatusNavigation).ToArray();
                users_dg.ItemsSource = clients;
            }
            alluses_tblock.Text = $"Всего - {users_dg.Items.Count}";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();
            alluses_tblock.Text = $"Всего - {users_dg.Items.Count}";
        }

        private void userAdd_but_Click(object sender, RoutedEventArgs e)
        {
            UserAdd win = new UserAdd();
            win.ShowDialog();
            LoadDateGrid();
        }

        private void search_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                User[] clients = db.Users.Where(o =>
                EF.Functions.Like(o.Login, $"%{search_tb.Text}%")).ToArray();
                users_dg.ItemsSource = clients;
            }
        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            User user = users_dg.SelectedItem as User;
            using (TimeFixerContext db = new TimeFixerContext())
                user = db.Users.Where(o => o.Ld == user.Ld).Include(o => o.IdSettingNavigation.IdStatusNavigation).First();

            MinWin win = new MinWin(new UserEdit_page(user));
            win.ShowDialog();
            LoadDateGrid();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            User user = users_dg.SelectedItem as User;
            if (MyMessageBox.Show("Внимание", "Вы точно хотите удалить этого пользователя?", MyMessageBoxOptions.YesNo) == false)
                return;

            using (TimeFixerContext db = new TimeFixerContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            MyMessageBox.Show("Внимание", "Пользователь успешно удален!");

            LoadDateGrid();
        }
    }
}
