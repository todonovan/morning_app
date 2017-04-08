using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorningApp
{
    public class MainWindowViewModel : BindableBase
    {
        private WeatherViewModel _weatherViewModel;

        private BindableBase _currentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public MainWindowViewModel()
        {
            _weatherViewModel = new WeatherViewModel();

            // Set default initial view model.
            CurrentViewModel = _weatherViewModel;
        }
    }
}
