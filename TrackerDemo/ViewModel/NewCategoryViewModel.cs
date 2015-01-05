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

    public class NewCategoryViewModel : ViewModelBase, IDataErrorInfo
    {
        public NewCategoryViewModel()
        {
            CreateNewCategoryCommand = new RelayCommand(CreateNewCategory, IsValid);
            Name = String.Empty;
            Unit = String.Empty;
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);
                CreateNewCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        private string unit;
        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
                RaisePropertyChanged(() => Unit);
                CreateNewCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreateNewCategoryCommand { get; set; }

        private void CreateNewCategory()
        {
            Messenger.Default.Send<NewCategoryMessage>(new NewCategoryMessage(new Category(Name, Unit)));
            Messenger.Default.Send<CloseWindowMessage>(new CloseWindowMessage());
            Name = String.Empty;
            Unit = String.Empty;
        }

        private bool IsValid()
        {
            return Error.Equals(string.Empty);
        }

        public string Error
        {
            get
            {
                var b = new StringBuilder();
                b.Append(this["Name"]);
                b.Append(this["Unit"]);
                return b.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        return Name == null || Name.Equals(String.Empty) ? "Enter a name" : String.Empty;
                    case "Unit":
                        return Unit == null || Unit.Equals(String.Empty) ? "Enter a unit" : String.Empty;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}