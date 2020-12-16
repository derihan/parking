using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModel
{
    public class AreavIewModels : INotifyPropertyChanged
    {
        private AreaServices _services;
        public AreavIewModels()
        {
            _services = new AreaServices();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<AreaModels> areas;
        public ObservableCollection<AreaModels> AreasList
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
            AreasList = new ObservableCollection<AreaModels>(_services.GetAll());
        }
    }
}
