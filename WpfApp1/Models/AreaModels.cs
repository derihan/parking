using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class AreaModels
    {

        private int areaId;

        public int AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        private int areaNumber;

        public int AreaNumber
        {
            get { return areaNumber; }
            set { areaNumber = value; }
        }

        private int areaSts;

        public int AreaSts
        {
            get { return areaSts; }
            set { areaSts = value; }
        }

        private int feesVal;

        public int FessValue
        {
            get { return feesVal; }
            set { feesVal = value; }
        }

        private string kategori;

        public string Kategori
        {
            get { return kategori; }
            set { kategori = value; }
        }

        private DateTime createdAt;

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

    }
}
