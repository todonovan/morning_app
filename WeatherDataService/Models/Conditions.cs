using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataService.Models
{
    public class Conditions
    {
        public CloudCover CloudCover { get; set; }
        public bool IsPrecip { get; set; }
        public PrecipType PrecipType { get; set; }
        public double Temperature { get; set; }
        public double FeelsLikeTemp { get; set; }
        public int RelativeHumidityPercent { get; set; }
        public double WindMPH { get; set; }
    }
}
