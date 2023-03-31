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
    public partial class ClockModelEdit_page : Page
    {
        ModelClock clock;
        public ClockModelEdit_page(ModelClock clock)
        {
            InitializeComponent();
            this.clock = clock;
        }
        public void LoadModelClockData()
        {
            model_tb.Text = clock.Model;
            manufacturer_tb.Text = clock.Manufacturer;
            description_tb.Text = clock.Description;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadModelClockData();
        }

        private void editClockModel_but_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(model_tb.Text) || string.IsNullOrEmpty(manufacturer_tb.Text))
            {
                MyMessageBox.Show("Ошибка", "Пожалуйста, заполните все обязательные поля перед обновлением модели часов.");
            }
            else
            {
                clock.Model = model_tb.Text;
                clock.Manufacturer = manufacturer_tb.Text;
                clock.Description = description_tb.Text;

                using (TimeFixerContext db = new TimeFixerContext())
                {
                    db.ModelClocks.Update(clock);
                    db.SaveChanges();
                }
                MyMessageBox.Show("Внимание", "Данные успешно обновлены!");
                Classes.Settings.window.Close();
            }
        }
    }
}
