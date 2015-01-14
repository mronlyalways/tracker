using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrackerDemo.Message;

namespace TrackerDemo.View
{
    [ValueConversion(typeof(NotificationMessage.NotificationType), typeof(string))]
    public class NotificationTypeToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is NotificationMessage.NotificationType)
            {
                var type = (NotificationMessage.NotificationType)value;
                switch(type)
                {
                    case NotificationMessage.NotificationType.Success :
                        return "\uE10B";
                    case NotificationMessage.NotificationType.Failure :
                        return "\uE10A";
                    case NotificationMessage.NotificationType.Warning :
                        return "\uE171";
                    case NotificationMessage.NotificationType.Sync :
                        return "\uE117";
                    case NotificationMessage.NotificationType.Info :
                        return "\uE129";
                    default :
                        return "\uE10C";
                }
            }
            else
            {
                return "\uE10C";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
