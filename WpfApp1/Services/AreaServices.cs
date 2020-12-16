using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class AreaServices
    {
        private static List<AreaModels> AreasList;
        private AreaModels _areamodel;
        public AreaServices()
        {
            AreasList = new List<AreaModels>();
            _areamodel = new AreaModels();
        }

        public List<AreaModels> GetAll()
        {
            var client = new RestClient("http://localhost:5000/api/data-area");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);
            return AreasList;
        }
    }
}
