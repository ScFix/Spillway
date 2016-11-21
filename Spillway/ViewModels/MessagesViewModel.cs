using Spillway.Models;
using System.Collections.Generic;
using System;
using Spillway.Services;
using System.Linq;
using Spillway.Interfaces;
using System.Windows.Input;
using Spillway.Utilities;
using System.Diagnostics;

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


        #region NavigateTo
        private ICommand _NavigateTo = null;
        public ICommand NavigateTo
        {
            get
            {
                if (_NavigateTo == null)
                {
                    _NavigateTo = new RelayCommand(NavigateToExecute, CanNavigateTo);
                }
                return _NavigateTo;
            }
        }

        private bool CanNavigateTo(object obj)
        {
            return true;
        }

        private void NavigateToExecute(object obj)
        {
            var notification = obj as Notification;
            if (notification != null)
            {
                //this shout be a different call to a shared code base with the notification that is launching on the toast click
                Process.Start(notification.Link);
            }
            #endregion //NavigateTo
        }
    }
}