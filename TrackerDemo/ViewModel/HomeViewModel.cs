using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrackerDemo.Data;
using TrackerDemo.Message;
using TrackerDemo.Model;

namespace TrackerDemo.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private ResourceLocator locator;

        public HomeViewModel()
        {
            NewCategoryCommand = new RelayCommand(NewCategory, () => true);
            OpenCategoryCommand = new RelayCommand(OpenCategory, () => true);
            locator = (ResourceLocator)App.Current.Resources["Locator"];

            Messenger.Default.Register<NewCategoryMessage>(this, OnNewCategoryCreated);

            Categories = new ObservableCollection<Category>(locator.GetInstance<IDataService>().Load());
        }

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }

        private Category selected;
        public Category Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public RelayCommand NewCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand OpenCategoryCommand
        {
            get;
            private set;
        }

        private void NewCategory()
        {
            Messenger.Default.Send(new RequestNewCategoryMessage());
        }

        private void OpenCategory()
        {
            locator.Chrome.Current = new CategoryViewModel(Selected, this);
        }

        private void OnNewCategoryCreated(NewCategoryMessage m)
        {
            Categories.Add(m.Category);
        }
    }
}