using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerDemo.Message
{
    public class NotificationMessage
    {
        public enum NotificationType
        {
            Success,
            Failure,
            Warning,
            Sync,
            Info
        }

        public NotificationMessage(String message, NotificationType type)
        {
            Message = message;
            Type = type;
        }

        public string Message { get; set; }

        public NotificationType Type { get; set; }
    }
}
