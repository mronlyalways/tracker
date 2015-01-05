using System.Windows.Controls;

namespace TrackerDemo.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public HomeView()
        {
            InitializeComponent();
            //Closing += (s, e) => ResourceLocator.Cleanup();
        }
    }
}