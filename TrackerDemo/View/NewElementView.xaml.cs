using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrackerDemo.Message;

namespace TrackerDemo.View
{
    /// <summary>
    /// Interaction logic for NewCategoryView.xaml
    /// </summary>
    public partial class NewElementView : Window
    {
        public NewElementView()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
        }

        private void CloseWindow(CloseWindowMessage m)
        {
            this.Close();
        }
    }
}
