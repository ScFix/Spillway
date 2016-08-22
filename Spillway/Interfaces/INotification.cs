using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Interfaces
{
	public interface INotification
	{
		int IsUnread { get; set; }
		long Date { get; set; }
		string Type { get; set; }
		string Link { get; set; }
	}
}
