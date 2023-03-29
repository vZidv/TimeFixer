using TimeFixer.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
//using TimeFixer.Database;
using Wpf.Ui.Controls;

namespace TimeFixer.View.Windows
{
    /// <summary>
    /// Interaction logic for Authorization_win.xaml
    /// </summary>
    public partial class Authorization_win : Window
    {
        public Authorization_win()
        {
            InitializeComponent();               
        }

        private void enter_but_Click(object sender, RoutedEventArgs e)
        {
            if (login_tb.Text == String.Empty || password_tb.Password == String.Empty)
            {
                MyMessageBox.Show("Ошибка", "Поле логин или пароль пустое!", true);
                return;
            }
            try
            {
                User user;

                using (TimeFixerContext db = new TimeFixerContext())
                    user = db.Users.Where(u => u.Login == login_tb.Text).Include(o => o.IdSettingNavigation.IdStatusNavigation).FirstOrDefault();

                if (user == null || password_tb.Password != user.Password)
                {
                    Wpf.Ui.Controls.MessageBox ms = new Wpf.Ui.Controls.MessageBox();
                    MyMessageBox.Show("Ошибка", "Поле логин или пароль пустое!", true);

                    return;
                }
                if (password_tb.Password != user.Password)
                {
                    Wpf.Ui.Controls.MessageBox ms = new Wpf.Ui.Controls.MessageBox();
                    MyMessageBox.Show("Ошибка", "Поле логин или пароль пустое!", true);
                }

                Classes.Settings.user = user;
                Main_win win = new Main_win();
                win.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Ошибка", ex.Message);
            }
        }
    }
}
