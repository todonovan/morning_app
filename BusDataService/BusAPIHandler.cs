using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDataService
{
    public class BusAPIHandler
    {
        private const string _urlBase = @"http://truetime.portauthority.org/bustime/api/v3/getpredictions/";
        private string _apiKey;
        private string _stopNumber;

        public BusAPIHandler()
        {
            _apiKey = ConfigurationManager.AppSettings["bus_api_key"];
            _stopNumber = ConfigurationManager.AppSettings["bus_stop_num"];
        }
    }
}
