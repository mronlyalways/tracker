using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrackerDemo.Data;
using TrackerDemo.Message;
using TrackerDemo.Model;
using TrackerDemo.View;

namespace TrackerDemo.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private IDataService data;
        private ChromeViewModel chrome;

        public HomeViewModel(IDataService data, ChromeViewModel chrome)
        {
            this.data = data;
            this.chrome = chrome;
            chrome.Current = this;
            Categories = new ObservableCollection<CategoryViewModel>(data.Load().Select(x => new CategoryViewModel(x, this, chrome)));

            OpenCategoryCommand = new RelayCommand(OpenCategory);
            RaiseNewCategoryCommand = new RelayCommand(RaiseNewCategory);
            RaiseNewElementCommand = new RelayCommand(RaiseNewElement);

            NewCategoryRequest = new InteractionRequest<ResultNotification<Category>>();
            NewElementRequest = new InteractionRequest<ResultNotification<Element>>();
        }

        private ObservableCollection<CategoryViewModel> categories;
        public ObservableCollection<CategoryViewModel> Categories
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

        private CategoryViewModel selected;
        public CategoryViewModel Selected
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

        public RelayCommand OpenCategoryCommand { get; private set; }

        public RelayCommand RaiseNewCategoryCommand { get; private set; }

        public RelayCommand RaiseNewElementCommand { get; private set; }

        public InteractionRequest<ResultNotification<Category>> NewCategoryRequest { get; private set; }

        public InteractionRequest<ResultNotification<Element>> NewElementRequest { get; private set; }

        private void OpenCategory()
        {
            chrome.Current = Selected;
        }

        private void RaiseNewCategory()
        {
            var notification = new ResultNotification<Category>() { Title = "New Category" };
            NewCategoryRequest.Raise(notification,
                returned =>
                {
                    data.Persist(returned.Result);
                    Categories.Add(new CategoryViewModel(returned.Result, this, chrome));
                    Messenger.Default.Send<TrackerDemo.Message.NotificationMessage>(new TrackerDemo.Message.NotificationMessage("New category created", Message.NotificationMessage.NotificationType.Success));
                });
        }

        private void RaiseNewElement()
        {
            var notification = new ResultNotification<Element>() { Title = "New Element" };
            NewElementRequest.Raise(notification,
                returned =>
                {
                    Selected.Category.Elements.Add(returned.Result);
                    data.Persist(Selected.Category);
                    Messenger.Default.Send<TrackerDemo.Message.NotificationMessage>(new TrackerDemo.Message.NotificationMessage("New element added", Message.NotificationMessage.NotificationType.Success));
                });
        }
    }
}