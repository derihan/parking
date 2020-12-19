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
using WpfApp1.ViewModel;

namespace WpfApp1.views
{
    /// <summary>
    /// Interaction logic for areaModel.xaml
    /// </summary>
    public partial class areaModel : Window
    {
        private AreavIewModels _areaModel;
        
        public areaModel(AreavIewModels areaModel)
        {
            InitializeComponent();
            _areaModel = areaModel;
            this.DataContext = _areaModel;
            _areaModel.LoadFees();
            _areaModel.LoadKategori();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }


      
    }
}
