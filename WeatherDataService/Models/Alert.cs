using System;

namespace WeatherDataService.Models
{
    public class Alert
    {
        public WarningType Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime Expires { get; set; }
        public string Message { get; set; }
        public WarningSignificance Significance { get; set; }
    }
}
