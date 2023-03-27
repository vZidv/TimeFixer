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
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();
            allClock_tblock.Text = $"Всего моделей - {ClockModelCount()}";
        }

        private int ClockModelCount()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                ModelClock[] modelClocks = db.ModelClocks.ToArray();
                return modelClocks.Length;
            }
        }

        private void clockModelAdd_but_Click(object sender, RoutedEventArgs e)
        {
            Windows.ClockModelAdd_win win = new Windows.ClockModelAdd_win();
            win.ShowDialog();
            LoadDateGrid();
        }
    }
}
