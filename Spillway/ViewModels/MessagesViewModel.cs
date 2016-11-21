using Spillway.Models;
using System.Collections.Generic;
using System;
using Spillway.Services;
using System.Linq;
using Spillway.Interfaces;

namespace Spillway.ViewModels
{
    public class MessagesViewModel : ViewModelBase, ISection
    {
        private Dictionary<string, Notification> _KnownNotifications = new Dictionary<string, Notification>();
        private ToastService toastService = new ToastService();

        public IList<Notification> Notifications
        {
            get
            {
                return _KnownNotifications.Values.ToList<Notification>();
            }
        }

        public string SectionName
        {
            get
            {
                return "All Notifications"; 
            }
        }

        internal void ProcessNotifications(object sender, StackArgs e)
        {
            foreach (var notification in e.Notifications)
            {
                string key = GetNotificationKey(notification);
                if (!_KnownNotifications.ContainsKey(key))
                {
                    _KnownNotifications.Add(key, notification);
                    toastService.ShowToast(notification);
                    // this can be optimized so that this is only called once but it doesn't matter for right now 
                    OnPropertyChanged("Notifications");
                }

            }
        }

        private string GetNotificationKey(Notification notification)
        {
            return notification.Link;
        }
    }
}