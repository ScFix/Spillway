using System;
using System.Collections.Generic;

namespace Spillway.Models
{
    public delegate void StackNotifyHandler(object sender, StackArgs e);

    public class StackArgs : EventArgs
    {
        public List<Notification> Notifications { get; set; }
    }
}