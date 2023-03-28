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

namespace TimeFixer.View.Windows
{
    /// <summary>
    /// Interaction logic for OrderAdd_win.xaml
    /// </summary>
    public partial class OrderAdd_win : Window
    {
        public OrderAdd_win()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Pages.OrderAdd_page());
            Classes.Settings.frame = mainFrame;
            Classes.Settings.window = this;
        }

    }
}
