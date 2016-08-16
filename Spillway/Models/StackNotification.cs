using Spillway.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Models
{
	public delegate void StackNotifyHandler(object sender, StackArgs e);

	public class StackArgs : EventArgs
	{
		public List<Notification> Notifications { get; set; }
	}

}
