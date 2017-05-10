using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherDataService.Models;

namespace WeatherDataService
{
    public class WeatherRepository
    {
        public List<Alert> Alerts { get; set; }
        public DateTime AlertsUpdatedTime { get; private set; }
        public Conditions Conditions { get; set; }
        public DateTime ConditionsUpdatedTime { get; private set; }
        public Forecast Forecast { get; set; }
        public DateTime ForecastUpdatedTime { get; private set; }

        private WeatherAPIHandler _apiHandler;

        public WeatherRepository()
        {
            _apiHandler = new WeatherAPIHandler();
        }

        public async Task ForceUpdateAllModels()
        {
            foreach (string s in new List<String> { "alerts", "conditions", "forecast" })
            {
                await UpdateModel(s);
            }
            AlertsUpdatedTime = DateTime.Now;
            ConditionsUpdatedTime = DateTime.Now;
            ForecastUpdatedTime = DateTime.Now;
        }

        public async Task UpdateModels()
        {
            if (DateTime.Now - AlertsUpdatedTime > TimeSpan.FromMinutes(2))
            {
                await UpdateModel("alerts");
                AlertsUpdatedTime = DateTime.Now;
            }

            if (DateTime.Now - ConditionsUpdatedTime > TimeSpan.FromMinutes(2))
            {
                await UpdateModel("conditions");
                ConditionsUpdatedTime = DateTime.Now;
            }

            if (DateTime.Now - ForecastUpdatedTime > TimeSpan.FromMinutes(2))
            {
                await UpdateModel("forecast");
                ForecastUpdatedTime = DateTime.Now;
            }
        }

        private async Task UpdateModel(string modelName)
        {
            string json;
            switch (modelName)
            {
                case "alerts":
                    json = await _apiHandler.MakeAPICallAsync("alerts");
                    Alerts = AlertsBuilder.Build(json);
                    break;
                case "conditions":
                    json = await _apiHandler.MakeAPICallAsync("conditions");
                    Conditions = ConditionsBuilder.Build(json);
                    break;
                case "forecast":
                    json = await _apiHandler.MakeAPICallAsync("forecast");
                    Forecast = ForecastBuilder.Build(json);
                    break;
            }
        }
    }
}
