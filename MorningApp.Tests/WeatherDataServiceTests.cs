using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataService;

namespace MorningApp.Tests
{
    [TestFixture]
    public class WeatherDataServiceTests
    {
        [Test]
        public void ShouldBuildProperAlert()
        {
            Dictionary<string, string> testAlertData = new Dictionary<string, string>
            {
                ["type"] = "HEA",
                ["date_epoch"] = "1341332040",
                ["expires_epoch"] = "1341662400",
                ["message"] = "\u000A...Heat advisory remains in effect until 8 am EST Saturday...\u000A",
                ["significance"] = "Y",
            };

            var testAlert = AlertsBuilder.Build(testAlertData);
            Assert.That(testAlert.Type, Is.EqualTo(WarningType.HeatAdv));
            Assert.That(testAlert.Significance, Is.EqualTo(WarningSignificance.Advisory));
        }

        [Test]
        public void ShouldBuildProperConditions()
        {
            Dictionary<string, string> testConditionsData = new Dictionary<string, string>
            {
                ["weather"] = "Partly Cloudy",
                ["temp_f"] = "66.3",
                ["relative_humidity"] = "65%",
                ["wind_mph"] = "22.0",
                ["feelslike_f"] = "66.3"
            };

            var testConditions = ConditionsBuilder.Build(testConditionsData);
            Assert.That(testConditions.CloudCover, Is.EqualTo(CloudCover.PartlyCloudy));
            Assert.That(testConditions.IsPrecip, Is.EqualTo(false));
            Assert.That(testConditions.PrecipType, Is.EqualTo(PrecipType.None));
            Assert.That(testConditions.Temperature, Is.EqualTo(66.3).Within(.01));
            Assert.That(testConditions.FeelsLikeTemp, Is.EqualTo(66.3).Within(.01));
            Assert.That(testConditions.RelativeHumidityPercent, Is.EqualTo(65));
            Assert.That(testConditions.WindMPH, Is.EqualTo(22.0).Within(.01));
        }

        [Test]
        public void ShouldHandleTrickyPrecipitation()
        {
            Dictionary<string, string> testConditionsData1 = new Dictionary<string, string>
            {
                ["weather"] = "Small Hail Showers",
                ["temp_f"] = "66.3",
                ["relative_humidity"] = "65%",
                ["wind_mph"] = "22.0",
                ["feelslike_f"] = "66.3"
            };

            var testOne = ConditionsBuilder.Build(testConditionsData1);
            Assert.That(testOne.IsPrecip, Is.EqualTo(true));
            Assert.That(testOne.PrecipType, Is.EqualTo(PrecipType.Hail));

            Dictionary<string, string> testConditionsData2 = new Dictionary<string, string>
            {
                ["weather"] = "Thunderstorms and Snow",
                ["temp_f"] = "66.3",
                ["relative_humidity"] = "65%",
                ["wind_mph"] = "22.0",
                ["feelslike_f"] = "66.3"
            };

            var testTwo = ConditionsBuilder.Build(testConditionsData2);
            Assert.That(testTwo.IsPrecip, Is.EqualTo(true));
            Assert.That(testTwo.PrecipType, Is.EqualTo(PrecipType.Storm));

            Dictionary<string, string> testConditionsData3 = new Dictionary<string, string>
            {
                ["weather"] = "Thunderstorms with Small Hail",
                ["temp_f"] = "66.3",
                ["relative_humidity"] = "65%",
                ["wind_mph"] = "22.0",
                ["feelslike_f"] = "66.3"
            };

            var testThree = ConditionsBuilder.Build(testConditionsData3);
            Assert.That(testThree.IsPrecip, Is.EqualTo(true));
            Assert.That(testThree.PrecipType, Is.EqualTo(PrecipType.Storm));
        }
    }
}
