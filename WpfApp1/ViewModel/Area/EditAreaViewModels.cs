
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;
using WpfApp1.Services;


namespace WpfApp1.ViewModel.Area
{
    public class EditAreaViewModels
    {

        AreaServices _services = new AreaServices();
        AreavIewModels areas = new AreavIewModels();

        private AreaModel areamodel;    

        public AreaModel AreaSelceted
        {
            get { return areamodel; }
            set { areamodel = value; }
        }

        private int selectKategori;

        public int SelectKategori
        {
            get { return selectKategori; }
            set { selectKategori = value; }
        }

    

        public EditAreaViewModels(AreaModel seletItem)
        {
           
            AreaSelceted = seletItem;
           
        }

        private ICommand editSave;

        public ICommand EditSaveCommand 
        {
            get {
                if (editSave == null)
                    editSave = new RelayCommand(Save);
                return editSave;
            }
           
        }

        public event EventHandler AreaEdit;

        private void Save()
        {
            Console.WriteLine(AreaSelceted.ParkFeesId);
        }

    }
}
