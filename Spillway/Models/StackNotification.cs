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
		public IList<StackNotification> Notifications { get; set; }
	}

	public class StackNotification
	{

	}
}
