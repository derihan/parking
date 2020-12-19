using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.Services
{
    public class AreaServices
    {
      
        private AreaModel _areamodel;
        private JObject _dataresponse;

        public AreaServices()
        {
        }

        public bool SaveData(AreaModel _area)
        {
            
                var client = new RestClient("http://localhost:5000/api/data-area");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", string.Format("Bearer {0}", new GetToken().getToken()));
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new
                {
                    AreaNumber = _area.AreaNumber,
                    AreaKategoriId = _area.KategoriId,
                    AreaParkingFeesId = _area.FessId
                });

                var response = client.Post(request);
                Console.WriteLine(response.StatusCode);
                if (response.IsSuccessful)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }

        public List<AreaModel> GetAll()
        {
           
            var client = new RestClient("http://localhost:5000/api/data-area");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", new GetToken().getToken()));
            IRestResponse response = client.Execute(request);
            
            if (response.IsSuccessful)
            {
                JObject o = JObject.Parse(response.Content);

                JArray a = (JArray)o["data"];

                List<AreaModel> person = a.ToObject<List<AreaModel>>();

                return person;
            }
            else
            {
                return new List<AreaModel>();
            }           
        }

       

    }
}
