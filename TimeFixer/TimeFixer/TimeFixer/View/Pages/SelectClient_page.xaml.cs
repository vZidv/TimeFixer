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
    /// Interaction logic for SelectClient_page.xaml
    /// </summary>
    public partial class SelectClient_page : Page
    {
        private IOreder order;
        public SelectClient_page(IOreder order)
        {
            InitializeComponent();
            this.order = order;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDateGrid();
        }

        private void LoadDateGrid()
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                Client[] clients = db.Clients.Include(o => o.IdHowDidFindUsNavigation).ToArray();
                client_dg.ItemsSource = clients;
            }
        }

        private void search_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            using (TimeFixerContext db = new TimeFixerContext())
            {
                Client[] clients = db.Clients.Where(o => 
                EF.Functions.Like(o.LastName,$"%{search_tb.Text}%") || 
                EF.Functions.Like(o.Name, $"%{search_tb.Text}%") ||
                EF.Functions.Like(o.Patronymic, $"%{search_tb.Text}%")).Include(o => o.IdHowDidFindUsNavigation).ToArray();
                client_dg.ItemsSource = clients;
            }
        }


        private int ChoosePersonId()
        {
            int r = client_dg.SelectedIndex;
            string id = null;

            for (int i = 0; i < 1;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemL = client_dg.Columns[i].GetCellContent(client_dg.Items[r]) as TextBlock;
                        id = itemL.Text;
                        break;
                }
                i++;
            }
            return Convert.ToInt32(id);
        }
        private Client ClientReturn(int id)
        {
            using (TimeFixerContext db = new TimeFixerContext())
                return db.Clients.Where(o => o.Id == id).Include(o => o.IdHowDidFindUsNavigation).FirstOrDefault();
        }
        private void client_dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int id = ChoosePersonId();
            order.client = ClientReturn(id);
            Classes.Settings.frame.GoBack();
        }

        private void back_but_Click(object sender, RoutedEventArgs e) => Classes.Settings.frame.GoBack();

    }
}
