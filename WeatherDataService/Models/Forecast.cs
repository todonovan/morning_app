using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataService.Models
{
    public class Forecast
    {
        public string Message { get; set; }
        public int HighTemp { get; set; }
        public int LowTemp { get; set; }
    }
}
