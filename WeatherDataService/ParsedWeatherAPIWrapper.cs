using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataService
{
    public class ParsedWeatherAPIWrapper
    {
        public Dictionary<string, string> Data { get; set; }

        public ParsedWeatherAPIWrapper(Dictionary<string, string> parsedJSON)
        {
            Data = parsedJSON;
        }
    }
}
