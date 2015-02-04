using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace TrackerDemo.ViewModel
{
    public class ChromeViewModel : ViewModelBase
    {
        private bool firstTimeLoaded;
        public ChromeViewModel()
        {
            firstTimeLoaded = true;
            LoadedCommand = new RelayCommand(Loaded, () => firstTimeLoaded);
        }

        public RelayCommand LoadedCommand { get; private set; }

        private ViewModelBase current;
        public ViewModelBase Current
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
                RaisePropertyChanged(() => Current);
            }
        }

        private void Loaded()
        {
            Current = ((ViewModelLocator)App.Current.Resources["Locator"]).Home;
            firstTimeLoaded = false;
        }
    }
}