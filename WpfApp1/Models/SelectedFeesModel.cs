using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class SelectedFeesModel
    {

        private int _parkfeesid;

        public int ParkFeesId
        {
            get { return _parkfeesid; }
            set { _parkfeesid = value; }
        }

        private int _parkFeesValue;

        public int ParkFeesValue
        {
            get { return _parkFeesValue; }
            set { _parkFeesValue = value; }
        }


    }
}
