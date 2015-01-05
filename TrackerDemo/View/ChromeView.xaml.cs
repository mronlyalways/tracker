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
    /// Interaction logic for ChromeView.xaml
    /// </summary>
    public partial class ChromeView : Window
    {
        public ChromeView()
        {
            InitializeComponent();
            Messenger.Default.Register<RequestNewCategoryMessage>(this, OnNewCategoryRequest);
            Messenger.Default.Register<RequestNewElementMessage>(this, OnNewElementRequest);
        }

        private void OnNewCategoryRequest(RequestNewCategoryMessage m)
        {
            var v = new NewCategoryView();
            v.Owner = this;
            v.ShowDialog();
        }

        private void OnNewElementRequest(RequestNewElementMessage m)
        {
            var v = new NewElementView();
            v.Owner = this;
            v.ShowDialog();
        }  
    }
}
