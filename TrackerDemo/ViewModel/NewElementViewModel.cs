using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Text;
using TrackerDemo.Message;
using TrackerDemo.Model;

namespace TrackerDemo.ViewModel
{
    public class NewElementViewModel : ViewModelBase, IDataErrorInfo
    {
        public NewElementViewModel()
        {
            CreateNewElementCommand = new RelayCommand(CreateNewElement, IsValid);
            Value = String.Empty;
            Date = DateTime.Now;
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
            Messenger.Default.Send<NewElementMessage>(new NewElementMessage(new Element(Convert.ToDouble(Value), Date)));
            Messenger.Default.Send<CloseWindowMessage>(new CloseWindowMessage());
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