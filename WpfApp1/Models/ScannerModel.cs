using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.Models
{
    public class ScannerModel
    {
        private string _license;
        public string License_number
        {
            get { return _license; }
            set { _license = value; }
        }

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }


    }
}
