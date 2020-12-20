using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Commands;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModel
{
    public class AreavIewModels : INotifyPropertyChanged
    {
        private AreaServices _services;
        private KategoriServices _katservice;
        private FeesServices _feeservices;
        private RelayCommand saveCommand;
        private RelayCommand _clicking;

        #region constructor
        public AreavIewModels()
        {
            
                
                _katservice = new KategoriServices();
                _feeservices = new FeesServices();
                CurrentArea = new AreaModel();
                saveCommand = new RelayCommand(Save);
                LoadKategori();
                LoadFees();
                _services = new AreaServices();
                LoadData();
           
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


        private SolidColorBrush colorBrush;

        public SolidColorBrush Coloring
        {
            get { return colorBrush; }
            set { colorBrush = value; OnPropertyChanged("Coloring"); }
        }


        #region save
        private AreaModel _areacurrent;
        public AreaModel CurrentArea
        {
            get { return _areacurrent; }
            set { _areacurrent = value; OnPropertyChanged("CurrentArea"); }
        }

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        public void Save(object sender)
        {

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
                 try
                {
                    var saving = _services.SaveData(CurrentArea);
                    if (saving)
                    {
                    Coloring = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                    Visibility = true;
                    Message = "Area saved";
                    }
                    else
                    {
                    Coloring = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                    Visibility = true;
                    Message = "Save data failed";
                    }
                }
                catch (Exception ex)
                {
                Coloring = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                Visibility = true;
                Message = ex.Message;
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
                OnPropertyChanged("areas");
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
