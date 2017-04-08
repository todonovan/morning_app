using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorningApp
{
    public class WeatherViewModel : BindableBase
    {
        private string _testString;
        public string TestString
        {
            get { return _testString; }
            set { SetProperty(ref _testString, value); }
        }

        public WeatherViewModel()
        {
            TestString = "Hi there!";
        }
    }
}
