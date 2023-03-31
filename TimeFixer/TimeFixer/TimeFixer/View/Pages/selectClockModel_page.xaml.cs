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

namespace TimeFixer.View.Pages
{
    /// <summary>
    /// Interaction logic for selectClockModel_page.xaml
    /// </summary>
    public partial class selectClockModel_page : Page
    {
        private IOreder order;
        public selectClockModel_page(IOreder order)
        {
            InitializeComponent();
            this.order = order;
        }
        private void LoadDateGrid()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                ModelClock[] modelClocks = db.ModelClocks.ToArray();
                clockModel_dg.ItemsSource = modelClocks;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();
        }

        private void back_but_Click(object sender, RoutedEventArgs e) => Classes.Settings.frame.GoBack();

        private void search_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                ModelClock[]  modelClocks = db.ModelClocks.Where(o =>
                EF.Functions.Like(o.Model, $"%{search_tb.Text}%") ||
                EF.Functions.Like(o.Manufacturer, $"%{search_tb.Text}%") 
                ).ToArray();
                clockModel_dg.ItemsSource = modelClocks;
            }
        }

        private void clockModel_dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int id = ChoosePersonId();
            order.clock = ModelClockReturn(id);
            Classes.Settings.frame.GoBack();
        }
        private ModelClock ModelClockReturn(int id)
        {
            using (TimeFixerContext db = new TimeFixerContext())
                return db.ModelClocks.Where(o => o.Id == id).FirstOrDefault();
        }
        private int ChoosePersonId()
        {
            int r = clockModel_dg.SelectedIndex;
            string id = null;

            for (int i = 0; i < 2;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemL = clockModel_dg.Columns[i].GetCellContent(clockModel_dg.Items[r]) as TextBlock;
                        id = itemL.Text;
                        break;
                }
                i++;
            }
            return Convert.ToInt32(id);
        }
    }
}
