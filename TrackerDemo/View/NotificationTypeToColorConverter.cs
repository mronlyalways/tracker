using System;
using System.Windows.Data;
using System.Windows.Media;
using TrackerDemo.Message;

namespace TrackerDemo.View
{
    [ValueConversion(typeof(NotificationMessage.NotificationType), typeof(Brush))]
    public class NotificationTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is NotificationMessage.NotificationType)
            {
                var type = (NotificationMessage.NotificationType)value;
                switch(type)
                {
                    case NotificationMessage.NotificationType.Success :
                        return Brushes.Green;
                    case NotificationMessage.NotificationType.Failure :
                        return Brushes.Red;
                    case NotificationMessage.NotificationType.Warning :
                        return Brushes.Orange;
                    case NotificationMessage.NotificationType.Sync :
                        return Brushes.Gray;
                    case NotificationMessage.NotificationType.Info :
                        return Brushes.Gray;
                    default :
                        return Brushes.Black;
                }
            }
            else
            {
                return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
