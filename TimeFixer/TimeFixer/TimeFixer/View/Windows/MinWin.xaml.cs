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
    /// Interaction logic for MinWin.xaml
    /// </summary>
    public partial class MinWin : Window
    {
        Page page;
        public MinWin(Page startPage)
        {
            InitializeComponent();

            page = startPage;
            Classes.Settings.frame = mainFrame;
            Classes.Settings.window = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(page);
        }
    }
}
