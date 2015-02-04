using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrackerDemo.Message;
using TrackerDemo.Model;
using TrackerDemo.View;

namespace TrackerDemo.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        private ViewModelBase parent;
        private ChromeViewModel chrome;

        public CategoryViewModel(Category category, ViewModelBase parent, ChromeViewModel chrome)
        {
            Category = category;
            this.parent = parent;
            this.chrome = chrome;

            Elements = new ObservableCollection<Element>(Category.Elements);
            OpenParentViewCommand = new RelayCommand(OpenParentView, () => true);
            RaiseNewElementCommand = new RelayCommand(RaiseNewElement, () => true);
            RaiseEditElementCommand = new RelayCommand(RaiseEditElement, () => true);
            DeleteElementCommand = new RelayCommand(DeleteElement, () => true);
            NewElementRequest = new InteractionRequest<ResultNotification<Element>>();
        }

        public Category Category { get; private set; }

        public ObservableCollection<Element> Elements { get; private set; }

        private Element selected;
        public Element Selected
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

        public double Goal
        {
            get
            {
                return Category.Goal;
            }
            set
            {
                Category.Goal = value;
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

        public RelayCommand OpenParentViewCommand { get; private set; }

        public RelayCommand RaiseNewElementCommand { get; private set; }

        public RelayCommand RaiseEditElementCommand { get; private set; }

        public RelayCommand DeleteElementCommand { get; private set; }

        public InteractionRequest<ResultNotification<Element>> NewElementRequest { get; private set; }

        private void OpenParentView()
        {
            chrome.Current = parent;
        }

        private void RaiseNewElement()
        {
            ResultNotification<Element> notification = new ResultNotification<Element>() { Title = "New Element" };
            NewElementRequest.Raise(notification,
                returned =>
                {
                    Elements.Add(returned.Result);
                    Category.Elements.Add(returned.Result);
                    RaisePropertyChanged(() => Avg);
                    RaisePropertyChanged(() => Min);
                    RaisePropertyChanged(() => Max);
                    Messenger.Default.Send<TrackerDemo.Message.NotificationMessage>(new TrackerDemo.Message.NotificationMessage("New element added", Message.NotificationMessage.NotificationType.Success));
                });
        }

        private void RaiseEditElement()
        {
            ResultNotification<Element> notification = new ResultNotification<Element>() { Title = "Edit Element" };
            var tmp = Selected; // keep reference to selected item
            notification.Result = tmp;
            NewElementRequest.Raise(notification,
                returned =>
                {
                    tmp.Value = returned.Result.Value;
                    tmp.Date = returned.Result.Date;
                    RaisePropertyChanged(() => Avg);
                    RaisePropertyChanged(() => Min);
                    RaisePropertyChanged(() => Max);
                });
        }

        private void DeleteElement()
        {
            Category.Elements.Remove(Selected);
            Elements.Remove(Selected);
            RaisePropertyChanged(() => Avg);
            RaisePropertyChanged(() => Min);
            RaisePropertyChanged(() => Max);
        }
    }
}
