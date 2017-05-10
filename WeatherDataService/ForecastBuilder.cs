using WeatherDataService.Models;
using Newtonsoft.Json.Linq;

namespace WeatherDataService
{
    public static class ForecastBuilder
    {
        public static Forecast Build(string rawJson)
        {
            JObject json = JObject.Parse(rawJson);
            JObject forecastDay = (JObject)json["forecast"]["simpleforecast"]["forecastday"][0];
            Forecast forecast = new Forecast();

            forecast.Message = (string)forecastDay["conditions"];
            forecast.HighTemp = (int)forecastDay["high"]["fahrenheit"];
            forecast.LowTemp = (int)forecastDay["low"]["fahrenheit"];

            return forecast;
        }
    }
}
