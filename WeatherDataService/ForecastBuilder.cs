using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataService.Models;

namespace WeatherDataService
{
    public static class ForecastBuilder
    {
        public static Forecast Build(ParsedWeatherAPIWrapper data)
        {
            Forecast forecast = new Forecast();
            forecast.Message = data.Data["forecast"];
            forecast.HighTemp = int.Parse(data.Data["high"]);
            forecast.LowTemp = int.Parse(data.Data["low"]);
            return forecast;
        }
    }
}
