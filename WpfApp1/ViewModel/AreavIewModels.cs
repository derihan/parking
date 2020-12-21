using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using WpfApp1.Interface;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.views;
using ownCommand = WpfApp1.Commands;

namespace WpfApp1.ViewModel
{
    public class AreavIewModels : INotifyPropertyChanged
    {
        private AreaServices _services;
        private KategoriServices _katservice;
        private FeesServices _feeservices;
        private Thread _thread;
        


        #region constructor
        public AreavIewModels()
        {

            _katservice = new KategoriServices();
            _feeservices = new FeesServices();
            CurrentArea = new AreaModel();
            SaveCommand = new RelayCommand<IClosable>(Save);
            getIdCommand = new ownCommand.RelayCommand(GetById);
            EditCommand = new RelayCommand<IClosable>(Edit);
            deleteCommand = new ownCommand.RelayCommand(Deleted);
            _services = new AreaServices();
            LoadData();
            LoadKategori();
            LoadFees();
        }
        #endregion

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private bool visibility;

        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; OnPropertyChanged("Visibility"); }
        }


        private AreaModel currentDelete;
            
        public AreaModel CurrentDelete
        {
            get { return currentDelete; }
            set { currentDelete = value; OnPropertyChanged("CurrentDelete"); }
        }


        private SolidColorBrush colorBrush;

        public SolidColorBrush Coloring
        {
            get { return colorBrush; }
            set { colorBrush = value; OnPropertyChanged("Coloring"); }
        }
        #region delet
        private ownCommand.RelayCommand deleteCommand;

        public ownCommand.RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void Deleted(object id)
        {
           
            var confirm = MessageBox.Show(string.Format("this data {0} has ben deleted are you sure ?", CurrentDelete.AreaNumber), "Confirm", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                var data = _services.Delete((int)id);
                if (data)
                {
                    Coloring = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                    Visibility = true;
                    Message = "Delete Data successfull";
                    LoadData();

                }
                else
                {
                    Coloring = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                    Visibility = true;
                    Message = "Delete data failed";
                }
            }

        }

        #endregion

        #region edit
        private ownCommand.RelayCommand getIdCommand;

        public ownCommand.RelayCommand GetIdCommand
        {
            get { return getIdCommand; }
        }

        public RelayCommand<IClosable> EditCommand
        {
            get;
            private set;
        }

        private AreaModel areabyid;
        public AreaModel Areabyid
        {
            get { return areabyid; }
            set { areabyid = value; OnPropertyChanged("Areabyid"); }
        }


        public void GetById(object id)
        {
            int ids = (int)id;
            Areabyid =_services.getById(ids);
            if (!String.IsNullOrEmpty(Areabyid.AreaId.ToString()))
            {
                AreaEditModal edited = new AreaEditModal();
                edited.Show();
            }
        }

        public async void Edit(IClosable window)
        {
            var id = currentDelete.AreaId;
            if (String.IsNullOrEmpty(CurrentArea.AreaNumber.ToString())
                || String.IsNullOrEmpty(CurrentArea.KategoriId.ToString())
                || String.IsNullOrEmpty(CurrentArea.FessId.ToString())
                )
            {
                Coloring = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                Visibility = true;
                Message = "Edit data failed";
               
            }
            else
            {
                Coloring = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                Visibility = true;
                Message = "Area saved";
                var cs = await delayid();
                if (cs)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #region save
        private AreaModel _areacurrent;
        public AreaModel CurrentArea
        {
            get { return _areacurrent; }
            set { _areacurrent = value; OnPropertyChanged("CurrentArea"); }
        }

        public RelayCommand<IClosable> SaveCommand
        {
            get;
            private set;
        }

    

        async Task<bool> delayid()
        {
            await Task.Delay(3000);
            return true;
        }

  
        public async void Save(IClosable window)
        {

            var canceakationTokenS = new CancellationTokenSource();
            var calcToken = canceakationTokenS.Token;


            if (String.IsNullOrEmpty(CurrentArea.AreaNumber.ToString()) 
                || String.IsNullOrEmpty(CurrentArea.KategoriId.ToString()) 
                || String.IsNullOrEmpty(CurrentArea.FessId.ToString())
                )
            {
                Coloring = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                Visibility = true;
                Message = "Save data failed";
            }
            else
            {
               
                var saving = _services.SaveData(CurrentArea);
                if (saving)
                {
                 
                    Coloring = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                    Visibility = true;
                    Message = "Area saved";
                    AreasList.Add(CurrentArea);
                    var cs = await delayid();
                    if (cs)
                    {
                        window.Close();
                    }
                }
                else
                {
                    Coloring = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                    Visibility = true;
                    Message = "Save data failed";
                }
              
            }
           
        }

        
        #endregion

        #region porperty notify
        public event PropertyChangedEventHandler PropertyChanged;
       

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        #region area data
        private ObservableCollection<AreaModel> areas;
        public ObservableCollection<AreaModel> AreasList
        {
            get { return areas; }
            set
            {
                areas = value;
                OnPropertyChanged("AreasList");
            }
        }

        private void LoadData()
        {
            AreasList = new ObservableCollection<AreaModel>(_services.GetAll());
        }
        #endregion
        #region data kategori
        private IEnumerable<KategoriModels> kategoris;
        public IEnumerable<KategoriModels> Kategoris
        {
            get { return kategoris; }
            set { kategoris = value; OnPropertyChanged("kategoris"); }
        }
        public void LoadKategori()
        {
            Kategoris = new List<KategoriModels>(_katservice.GetAll());
        }
        #endregion
        #region data fees
        private IEnumerable<FeesModel> feesModel;
        private object areasVi;
        private AreaViews _areaViews;

        public IEnumerable<FeesModel> FeesModels
        {
            get { return feesModel; }
            set { feesModel = value; OnPropertyChanged("feesModel"); }
        }
        public void LoadFees()
        {
            FeesModels = new List<FeesModel>(_feeservices.GetFees());
        }
        #endregion

     
    }
}
