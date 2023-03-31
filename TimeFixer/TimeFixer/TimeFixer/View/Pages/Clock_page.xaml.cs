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
    /// Interaction logic for Clock_page.xaml
    /// </summary>
    public partial class Clock_page : Page
    {
        public Clock_page()
        {
            InitializeComponent();
        }
        private void LoadDateGrid()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                ModelClock[] modelClocks = db.ModelClocks.ToArray();
                clock_dg.ItemsSource = modelClocks;
            }
            allClock_tblock.Text = $"Всего - {clock_dg.Items.Count}";
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();            
        }

        private void clockModelAdd_but_Click(object sender, RoutedEventArgs e)
        {
            Windows.ClockModelAdd_win win = new Windows.ClockModelAdd_win();
            win.ShowDialog();
            LoadDateGrid();
        }

        private void search_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                ModelClock[] clients = db.ModelClocks.Where(o =>
                EF.Functions.Like(o.Model, $"%{search_tb.Text}%") ||
                EF.Functions.Like(o.Manufacturer, $"%{search_tb.Text}%") ||
                EF.Functions.Like(o.Description, $"%{search_tb.Text}%")).ToArray();
                clock_dg.ItemsSource = clients;

                allClock_tblock.Text = $"Всего - {clock_dg.Items.Count}"; ;
            }
        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            ModelClock clock = clock_dg.SelectedItem as ModelClock;
            MinWin win = new MinWin(new ClockModelEdit_page(clock));
            win.ShowDialog();
            LoadDateGrid();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            ModelClock clock = clock_dg.SelectedItem as ModelClock;
            if (MyMessageBox.Show("Внимание", "Вы точно хотите удалить эту модель?", MyMessageBoxOptions.YesNo) == false)
                return;

            using (TimeFixerContext db = new TimeFixerContext())
            {
                db.ModelClocks.Remove(clock);
                db.SaveChanges();
            }
            MyMessageBox.Show("Внимание", "Модель успешно удалена!");

            LoadDateGrid();
        }
    }
}
