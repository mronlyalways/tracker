using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
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
            SelectedColor = new SolidColorBrush(Colors.Blue);
            ColorPalette = new ObservableCollection<SolidColorBrush>();
            ColorPalette.Add(SelectedColor);
            ColorPalette.Add(new SolidColorBrush(Colors.Orange));
            ColorPalette.Add(new SolidColorBrush(Colors.Purple));
            ColorPalette.Add(new SolidColorBrush(Colors.Green));
            ColorPalette.Add(new SolidColorBrush(Colors.Red));
            ColorPalette.Add(new SolidColorBrush(Colors.Gray));
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

        private ObservableCollection<SolidColorBrush> colorPalette;
        public ObservableCollection<SolidColorBrush> ColorPalette
        {
            get
            {
                return colorPalette;
            }
            set
            {
                colorPalette = value;
                RaisePropertyChanged(() => ColorPalette);
            }
        }

        private SolidColorBrush selectedColor;
        public SolidColorBrush SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                RaisePropertyChanged(() => SelectedColor);
                CreateNewCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreateNewCategoryCommand { get; set; }

        private void CreateNewCategory()
        {
            Messenger.Default.Send<NewCategoryMessage>(new NewCategoryMessage(new Category(Name, Unit, SelectedColor)));
            Messenger.Default.Send<TrackerDemo.Message.NotificationMessage>(new TrackerDemo.Message.NotificationMessage("New category created", Message.NotificationMessage.NotificationType.Success));
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
                b.Append(this["SelectedColor"]);
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
                    case "SelectedColor":
                        return SelectedColor == null ? "Pick a color" : String.Empty;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}