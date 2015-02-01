using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.ComponentModel;
using System.Text;
using TrackerDemo.Message;
using TrackerDemo.Model;

namespace TrackerDemo.ViewModel
{
    public class NewElementViewModel : ViewModelBase, IInteractionRequestAware, IDataErrorInfo
    {
        public NewElementViewModel()
        {
            CreateNewElementCommand = new RelayCommand(CreateNewElement, IsValid);
            Value = String.Empty;
            Date = DateTime.Now;
        }

        public Action FinishInteraction { get; set; }

        public INotification Notification
        {
            get
            {
                return TypedNotification;
            }
            set
            {
                if (value is ResultNotification<Element>)
                {
                    TypedNotification = value as ResultNotification<Element>;
                }
            }
        }

        private ResultNotification<Element> typedNotification;
        public ResultNotification<Element> TypedNotification
        {
            get
            {
                return typedNotification;
            }
            set
            {
                typedNotification = value;
                if (typedNotification.Result != null)
                {
                    Value = typedNotification.Result.Value.ToString();
                    Date = typedNotification.Result.Date;
                }
            }
        }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
                CreateNewElementCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged(() => Date);
                CreateNewElementCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreateNewElementCommand { get; set; }

        private void CreateNewElement()
        {
            if (TypedNotification.Result == null)
            {
                TypedNotification.Result = new Element();
            }

            TypedNotification.Result.Value = Convert.ToDouble(Value);
            TypedNotification.Result.Date = Date;
            FinishInteraction();
        }

        private bool IsValid()
        {
            return Error.Equals(String.Empty);
        }

        public string Error
        {
            get
            {
                var b = new StringBuilder();
                b.Append(this["Value"]);
                b.Append(this["Date"]);
                return b.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Value":
                        double d;
                        if (Double.TryParse(Value, out d))
                        {
                            return String.Empty;
                        }
                        else
                        {
                            return "Not a number";
                        }
                    case "Date":
                        return Date == null ? "Pick a date" : String.Empty;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}