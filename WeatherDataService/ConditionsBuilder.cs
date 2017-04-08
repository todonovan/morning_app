using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataService.Models;

namespace WeatherDataService
{
    public enum CloudCover
    {
        Clear,
        PartlyCloudy,
        MostlyCloudy,
        Overcast,
        ScatteredClouds,
        Precipitating
    }

    /// <summary>
    /// Uses Rain as a catchall for all liquid precip types that aren't
    /// drizzles. Storms are limited to thunderstorms.
    /// </summary>
    public enum PrecipType
    {
        Rain,
        Drizzle,
        Snow,
        Storm,
        FreezingRain,
        Hail,
        None
    }

    /// <summary>
    /// Constructs a Conditions model object, given a Dictionary of k/v returned
    /// from deserialized JSON request.
    /// </summary>
    public static class ConditionsBuilder
    {
        public static Conditions Build(ParsedWeatherAPIWrapper data)
        {
            Conditions currentConditions = new Conditions();
            string weatherPhrase = data.Data["weather"];
            currentConditions.CloudCover = ParseCloudCover(weatherPhrase);
            if (currentConditions.CloudCover == CloudCover.Precipitating)
            {
                currentConditions.IsPrecip = true;
                if (weatherPhrase.Contains("Freezing")) currentConditions.PrecipType = PrecipType.FreezingRain;
                else if (weatherPhrase.Contains("Drizzle")) currentConditions.PrecipType = PrecipType.Drizzle;
                else if (weatherPhrase.Contains("Thunderstorm") || weatherPhrase.Contains("Thunderstorms")) currentConditions.PrecipType = PrecipType.Storm;
                else if (weatherPhrase.Contains("Hail")) currentConditions.PrecipType = PrecipType.Hail;
                else if (weatherPhrase.Contains("Snow")) currentConditions.PrecipType = PrecipType.Snow;
                else currentConditions.PrecipType = PrecipType.Rain;
            }
            else
            {
                currentConditions.IsPrecip = false;
                currentConditions.PrecipType = PrecipType.None;
            }

            currentConditions.Temperature = double.Parse(data.Data["temp_f"]);
            currentConditions.FeelsLikeTemp = double.Parse(data.Data["feelslike_f"]);
            currentConditions.RelativeHumidityPercent = int.Parse(data.Data["relative_humidity"].Substring(0, 2));
            currentConditions.WindMPH = double.Parse(data.Data["wind_mph"]);

            return currentConditions;
        }

        private static CloudCover ParseCloudCover(string weatherPhrase)
        {
            if (weatherPhrase == "Partly Cloudy") return CloudCover.PartlyCloudy;
            else if (weatherPhrase == "Overcast") return CloudCover.Overcast;
            else if (weatherPhrase == "Clear") return CloudCover.Clear;
            else if (weatherPhrase == "Mostly Cloudy") return CloudCover.MostlyCloudy;
            else if (weatherPhrase == "Scattered Clouds") return CloudCover.ScatteredClouds;
            else return CloudCover.Precipitating;
        }
    }
}
