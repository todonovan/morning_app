using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataService.Models
{
    public class Alerts
    {
        public WarningType Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime Expires { get; set; }
        public string Message { get; set; }
        public WarningSignificance Significance { get; set; }
    }
}
