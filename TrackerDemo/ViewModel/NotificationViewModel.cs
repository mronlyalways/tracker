using GalaSoft.MvvmLight;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using TrackerDemo.Message;

namespace TrackerDemo.ViewModel
{
    public class NotificationViewModel : ViewModelBase, IDisposable
    {
        private ConcurrentQueue<NotificationMessage> pendingNotifications;
        private BackgroundWorker worker;

        public NotificationViewModel()
        {
            pendingNotifications = new ConcurrentQueue<NotificationMessage>();
            worker = new BackgroundWorker();
            MessengerInstance.Register<NotificationMessage>(this, ReceiveMessage);
            worker.DoWork += Notify;
        }

        private string notificationMessage;
        public string NotificationMessage
        {
            get
            {
                return notificationMessage;
            }
            set
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    notificationMessage = "";
                    RaisePropertyChanged(() => NotificationMessage);
                    notificationMessage = value;
                    RaisePropertyChanged(() => NotificationMessage);
                }), null);
            }
        }

        private NotificationMessage.NotificationType notificationType;
        public NotificationMessage.NotificationType NotificationType
        {
            get
            {
                return notificationType;
            }
            set
            {
                notificationType = value;
                RaisePropertyChanged(() => NotificationType);
            }
        }

        private void ReceiveMessage(NotificationMessage n)
        {
            pendingNotifications.Enqueue(n);
            
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        private void Notify(object sender, DoWorkEventArgs e)
        {
            NotificationMessage n;

            while (!pendingNotifications.IsEmpty)
            {
                pendingNotifications.TryDequeue(out n);
                NotificationType = n.Type;
                NotificationMessage = n.Message;
                Thread.Sleep(3000);
            }
        }

        public void Dispose()
        {
            worker.DoWork -= Notify;
        }
    }
}