using Spillway.Models;
using System.Collections.Generic;

namespace Spillway.ViewModels
{
    public class MessagesViewModel : ViewModelBase
    {
        private Dictionary<string, Notification> _KnownNotifications = new Dictionary<string, Notification>();

        internal void ProcessNotifications(object sender, StackArgs e)
        {
            foreach (var notification in e.Notifications)
            {
                if (!_KnownNotifications.ContainsKey(notification.Link))
                {
                }
                //notification.Link
            }
        }
    }
}