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
using WpfApp1.views;

namespace WpfApp1.views
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        ScannerPage scanner;
        public DashboardPage()
        {
            InitializeComponent();
            navigatedPage("/views/HomePage.xaml");
            scanner = new ScannerPage(null);
        }

        private void navigatedPage(string urif)
        {
            ContentFrame.Navigate(new Uri(urif, UriKind.RelativeOrAbsolute));
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            navigatedPage("/views/HomePage.xaml");
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
             navigatedPage("/views/ScannerMgm.xaml");
             
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            navigatedPage("/views/AreaViews.xaml");
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            navigatedPage("/views/KategoriView.xaml");
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            navigatedPage("/views/FeesViews.xaml");
        }
    }
}
