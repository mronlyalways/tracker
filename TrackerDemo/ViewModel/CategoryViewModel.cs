using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerDemo.Message;
using TrackerDemo.Model;

namespace TrackerDemo.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        private ResourceLocator locator;
        private ViewModelBase parent;

        public CategoryViewModel(Category category, ViewModelBase parent)
        {
            Category = category;
            locator = (ResourceLocator)App.Current.Resources["Locator"];
            this.parent = parent;

            Elements = new ObservableCollection<Element>(Category.Elements);
            CreateNewElementCommand = new RelayCommand(CreateNewElement, () => true);
            OpenParentViewCommand = new RelayCommand(OpenParentView, () => true);

            Messenger.Default.Register<NewElementMessage>(this, OnNewElementCreated);
        }

        private Category category;
        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        private ObservableCollection<Element> elements;
        public ObservableCollection<Element> Elements
        {
            get
            {
                return elements;
            }
            set
            {
                elements = value;
                RaisePropertyChanged(() => Elements);
            }
        }

        private double goal;
        public double Goal
        {
            get
            {
                return goal;
            }
            set
            {
                goal = value;
                RaisePropertyChanged(() => Goal);
            }
        }

        public double Avg
        {
            get
            {
                return Elements.Count == 0 ? 0 : Elements.Select(e => e.Value).Average();
            }
        }

        public double Min
        {
            get
            {
                return Elements.Count == 0 ? 0 : Elements.Select(e => e.Value).Min();
            }
        }

        public double Max
        {
            get
            {
                return Elements.Count == 0 ? 0 : Elements.Select(e => e.Value).Max();
            }
        }

        public RelayCommand CreateNewElementCommand
        {
            get;
            private set;
        }

        public RelayCommand OpenParentViewCommand
        {
            get;
            private set;
        }

        private void CreateNewElement()
        {
            Messenger.Default.Send(new RequestNewElementMessage());
        }

        private void OpenParentView()
        {
            locator.Chrome.Current = parent;
        }

        private void OnNewElementCreated(NewElementMessage m)
        {
            Category.Elements.Add(m.Element);
            Elements.Add(m.Element);
            RaisePropertyChanged(() => Avg);
            RaisePropertyChanged(() => Min);
            RaisePropertyChanged(() => Max);
        }
    }
}
