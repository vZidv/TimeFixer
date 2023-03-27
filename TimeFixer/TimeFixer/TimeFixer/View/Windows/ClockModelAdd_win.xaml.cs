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
    /// Interaction logic for ClockModelAdd_win.xaml
    /// </summary>
    public partial class ClockModelAdd_win : Window
    {
        public ClockModelAdd_win()
        {
            InitializeComponent();
        }

        private void addClockModel_but_Click(object sender, RoutedEventArgs e)
        {
            ModelClock modelClock = new ModelClock()
            {
                Model = model_tb.Text,
                Manufacturer = manufacturer_tb.Text,
                Description = description_tb.Text,
            };

            using (TimeFixerContext db = new TimeFixerContext())
            {
                db.ModelClocks.Add(modelClock);
                db.SaveChanges();
            }
            MyMessageBox.Show("Внимание", "Модель добавлена!", MyMessageBoxOptions.Ok);
            this.Close();
        }
    }
}
