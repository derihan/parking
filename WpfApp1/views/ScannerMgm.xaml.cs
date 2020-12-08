using System;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Emgu.CV;
using Emgu.CV.Structure;

using RestSharp;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Emgu.CV.Util;
using Emgu.Util;
using WpfApp1;
using WpfApp1.converters;
using Application = System.Windows.Forms.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp1.views
{
    /// <summary>
    /// Interaction logic for ScannerMgm.xaml
    /// </summary>
    public partial class ScannerMgm : Page
    {
        Colorcvt Cvtc;
        Image<Bgr, byte> imgInput;
        Image<Gray, byte> imgOut;
        Image<Gray, byte> imgBinarisasi;
        Image<Gray, byte> imgGray;
        private Bitmap img;
        private string filedirs;
        private string base64String;
        bool show = false;

      
        public ScannerMgm()
        {
            InitializeComponent();
            Cvtc = new Colorcvt();
        }

        private void GridopenScann_window(object sender, MouseButtonEventArgs e)
        {
            ScannerPage scPage = new ScannerPage();
           
            scPage.Show();
        }


        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                imgInput = new Image<Bgr, byte>(opf.FileName);
                pictureBox1.Image = imgInput.ToBitmap();
            }
            
            callApi(opf);
         
        }

        private void callApi(OpenFileDialog opf)
        {
            if (imgInput == null)
            {
                MessageBox.Show("File imgae wajib di isi");
                return;
            }
            else
            {
                filedirs = opf.FileName;
                var client = new RestClient("http://localhost:8000/ocr-service/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Connection", "Keep-Alive");
                request.AddFile("filed", filedirs);
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var jObject = JObject.Parse(response.Content);

                    var content = client.Execute(request).Content;
                    var deserialize = new JsonDeserializer();
                    var output = deserialize.Deserialize<Dictionary<String, string>>(response);
                    var result = output["base64_data"];
                    Base64toImage(result);
                    plate_recognize();
                }
                else
                {
                    Console.WriteLine("server error");
                }
            }

        }

        public System.Drawing.Image Base64toImage(string base64String)
        {
            byte[] imageByte = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream(imageByte, 0, imageByte.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                //return image;
                pictureBox2.Width = image.Width;
                pictureBox2.Height = image.Height;
                pictureBox2.Image = image;
                return image;
            }
        }

        public byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] result = null;

            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, bitmap.RawFormat);
                result = stream.ToArray();
            }

            return result;
        }

        public void plate_recognize()
        {
            Bitmap mastImg = new Bitmap(pictureBox2.Image);
            Image<Bgr, byte> imgBit = mastImg.ToImage<Bgr, byte>();
            //Image<Hsv, byte> grayin = imgBit.Convert<Hsv, byte>();
            Image<Gray, byte> grayin = imgBit.Convert<Gray, byte>();
            grayin._ThresholdBinaryInv(new Gray(80), new Gray(255));
            pictureBox3.Image = grayin.ToBitmap();
            pictureBox3.Width = grayin.Width;
            pictureBox3.Height = grayin.Height;
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }



        public void appluRangeFilter(int aa,int bb)
        {
           

        }

        public void pictclicked(object sender, EventArgs e)
        {
            Window1 ove = new Window1(this);

            ove.Show();
        }

       
    }
}
