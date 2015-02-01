using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
using TrackerDemo.Message;
using TrackerDemo.Model;

namespace TrackerDemo.ViewModel
{
    public class NewCategoryViewModel : ViewModelBase, IInteractionRequestAware, IDataErrorInfo
    {
        public NewCategoryViewModel()
        {
            CreateNewCategoryCommand = new RelayCommand(CreateNewCategory, IsValid);
            Name = String.Empty;
            Unit = String.Empty;
            ColorPalette = new ObservableCollection<SolidColorBrush>();
            SelectedColor = new SolidColorBrush(Colors.Blue);
            ColorPalette.Add(SelectedColor);
            ColorPalette.Add(new SolidColorBrush(Colors.Orange));
            ColorPalette.Add(new SolidColorBrush(Colors.Purple));
            ColorPalette.Add(new SolidColorBrush(Colors.Green));
            ColorPalette.Add(new SolidColorBrush(Colors.Red));
            ColorPalette.Add(new SolidColorBrush(Colors.Gray));
        }

        private Action finish;
        public Action FinishInteraction
        {
            get
            {
                return finish;
            }
            set
            {
                finish = value;
            }
        }

        public INotification Notification
        {
            get
            {
                return TypedNotification;
            }
            set
            {
                if (value is ResultNotification<Category>)
                {
                    TypedNotification = value as ResultNotification<Category>;
                }
            }
        }

        private ResultNotification<Category> typedNotification;
        public ResultNotification<Category> TypedNotification
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
                    Name = typedNotification.Result.Name;
                    Unit = typedNotification.Result.Unit;
                    SelectedColor = typedNotification.Result.Color;
                }
            }
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
            if (TypedNotification.Result == null)
            {
                TypedNotification.Result = new Category();
            }

            TypedNotification.Result.Name = Name;
            TypedNotification.Result.Unit = Unit;
            TypedNotification.Result.Color = SelectedColor;

            FinishInteraction();
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