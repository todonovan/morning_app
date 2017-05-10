using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataService;
using WeatherDataService.Models;

namespace MorningApp.Tests
{
    [TestFixture]
    public class WeatherDataServiceTests
    {
        [Test]
        public void ShouldBuildProperAlert()
        {
            string testAlertData = @"{
                'response': {},
                'alerts': [
                  {
                    'type': 'HEA',
                    'date_epoch': 1341332040,
                    'expires_epoch': 1341552400,
                    'message': '\n...Heat advisory remains in effect until 8 am EST Saturday...\n',
                    'significance': 'Y'
                  }
                ]
            }";

            List<Alert> testAlerts = AlertsBuilder.Build(testAlertData);
            Assert.That(testAlerts[0].Type, Is.EqualTo(WarningType.HeatAdv));
            Assert.That(testAlerts[0].Significance, Is.EqualTo(WarningSignificance.Advisory));
        }

        [Test]
        public void ShouldBuildProperConditions()
        {
            string testConditionsData = @"{
                'response': {},
                'current_observation': {
                    'weather': 'Partly Cloudy',
                    'temp_f': 66.3,
                    'relative_humidity': '65%',
                    'wind_mph': 22.0,
                    'feelslike_f': 66.3
                }
            }";

            var testConditions = ConditionsBuilder.Build(testConditionsData);
            Assert.That(testConditions.CloudCover, Is.EqualTo(CloudCover.PartlyCloudy));
            Assert.That(testConditions.IsPrecip, Is.EqualTo(false));
            Assert.That(testConditions.PrecipType, Is.EqualTo(PrecipType.None));
            Assert.That(testConditions.Temperature, Is.EqualTo(66.3).Within(.01));
            Assert.That(testConditions.FeelsLikeTemp, Is.EqualTo(66.3).Within(.01));
            Assert.That(testConditions.RelativeHumidity, Is.EqualTo("65%"));
            Assert.That(testConditions.WindMPH, Is.EqualTo(22.0).Within(.01));
        }

        [Test]
        public void ShouldHandleTrickyPrecipitation()
        {
            string testConditionsData1 = @"{
                'response': {},
                'current_observation': {
                    'weather': 'Small Hail Showers',
                    'temp_f': 66.3,
                    'relative_humidity': '65%',
                    'wind_mph': 22.0,
                    'feelslike_f': 66.3
                }
            }";

            var testOne = ConditionsBuilder.Build(testConditionsData1);
            Assert.That(testOne.IsPrecip, Is.EqualTo(true));
            Assert.That(testOne.PrecipType, Is.EqualTo(PrecipType.Hail));

            string testConditionsData2 = @"{
                'response': {},
                'current_observation': {
                    'weather': 'Thunderstorms and Snow',
                    'temp_f': 66.3,
                    'relative_humidity': '65%',
                    'wind_mph': 22.0,
                    'feelslike_f': 66.3
                }
            }";

            var testTwo = ConditionsBuilder.Build(testConditionsData2);
            Assert.That(testTwo.IsPrecip, Is.EqualTo(true));
            Assert.That(testTwo.PrecipType, Is.EqualTo(PrecipType.Storm));

            string testConditionsData3 = @"{
                'response': {},
                'current_observation': {
                    'weather': 'Thunderstorms with Small Hail',
                    'temp_f': 66.3,
                    'relative_humidity': '65%',
                    'wind_mph': 22.0,
                    'feelslike_f': 66.3
                }
            }";

            var testThree = ConditionsBuilder.Build(testConditionsData3);
            Assert.That(testThree.IsPrecip, Is.EqualTo(true));
            Assert.That(testThree.PrecipType, Is.EqualTo(PrecipType.Storm));
        }

        [Test]
        public void ShouldBuildProperForecast()
        {
            string testForecastData = @"{
                'response': {},
                'forecast': {
                    'simpleforecast': {
                        'forecastday': [
                            {
                                'conditions': 'Partly Cloudy',
                                'high': { 'fahrenheit': '65', 'celsius': '18' },
                                'low': { 'fahrenheit': '43', 'celsius': '6' }
                            }
                        ]
                    }
                }
            }";

            Forecast testForecast = ForecastBuilder.Build(testForecastData);
            Assert.That(testForecast.Message, Is.EqualTo("Partly Cloudy"));
            Assert.That(testForecast.HighTemp, Is.EqualTo(65));
            Assert.That(testForecast.LowTemp, Is.EqualTo(43));
        }
    }
}
