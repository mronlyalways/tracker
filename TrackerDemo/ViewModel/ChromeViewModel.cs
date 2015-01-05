using GalaSoft.MvvmLight;

namespace TrackerDemo.ViewModel
{
    public class ChromeViewModel : ViewModelBase
    {
        public ChromeViewModel()
        {
            Current = ((ResourceLocator)App.Current.Resources["Locator"]).Home;
        }

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
    }
}