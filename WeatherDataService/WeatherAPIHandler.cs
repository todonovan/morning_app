using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WeatherDataService
{
    public class WeatherAPIHandler
    {
        private Dictionary<string, string> _urlParams;
        private const string _urlBase = @"http://api.wunderground.com/api/";
        private string _apiKey;

        public WeatherAPIHandler()
        {
            _apiKey = ConfigurationManager.AppSettings["weather_api_key"];
            _urlParams = new Dictionary<string, string>()
            {
                ["alerts"] = _urlBase + _apiKey + @"/alerts/q/15218.json",
                ["conditions"] = _urlBase + _apiKey + @"/conditions/q/15218.json",
                ["forecast"] = _urlBase + _apiKey + @"/forecast/q/15218.json"
            };
        }

        public async Task<string> MakeAPICallAsync(string modelType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_urlBase);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                string jsonResult;

                var response = await client.GetAsync(_urlParams[modelType]);
                if (response.IsSuccessStatusCode)
                {
                    jsonResult = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    jsonResult = $"{(int)response.StatusCode} ({response.ReasonPhrase})";
                }

                return jsonResult;
            }
        }
    }
}
