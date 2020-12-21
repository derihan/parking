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
using WpfApp1.Interface;
using WpfApp1.ViewModel;

namespace WpfApp1.views
{
    /// <summary>
    /// Interaction logic for areaModel.xaml
    /// </summary>
    public partial class AreaAddModel : Window, IClosable
    {
        private AreavIewModels _areaModel;
       
        
        public AreaAddModel()
        {
           
            InitializeComponent();
            _areaModel = new AreavIewModels();
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
