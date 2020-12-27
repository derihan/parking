using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Services;
using WpfApp1.Models;
using QRCoder;
using System.Drawing;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using WpfApp1.views;
using WpfApp1.helper;

namespace WpfApp1.ViewModel.Scanner
{
    public class ScannerViewModel : INotifyPropertyChanged
    {
        ScannerServices scannerServices = new ScannerServices();
        private GenerateQRcode generatecodeQr;
        public ScannerViewModel()
        {
            generatecodeQr = new GenerateQRcode();
        }

        private string license;
        public string License
        {
            get { return license; }
            set { license = value;  OnPropertyChanged("License"); }
        }

        private ScannerModel images;
        public ScannerModel Images
        {
            get { return images; }
            set { images = value; OnPropertyChanged("Images"); }
        }

        private ICommand generateCommand;
        public ICommand GenerateCommand
        {
            get
            {
                if (generateCommand == null)
                    generateCommand = new RelayCommand(generateQr);
                return generateCommand;
            }
        }

      

        ScannerPage scanerwindow;

        public void generateQr()
        {
            
            Images = new ScannerModel { ImageSource = generatecodeQr.makeQrCode("asdhjbadbiwuyaqdniuiubguiwsebfiuweg") };


            if (scanerwindow == null)
            {
                scanerwindow = new ScannerPage(Images);
                scanerwindow.generateViewModel.scaneropen += ScannerOpen;
                scanerwindow.Show();
            }
            else
            {
                scanerwindow.Focus();
            }

            //Console.WriteLine(License);
            //scannerServices.reqQrCode(License);
        }

      

        private void ScannerOpen(object sender, EventArgs e)
        {
            scanerwindow.Close();


        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
