using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Models
{
	[DataContract()]
	public class Mail
	{
		[DataMember(Name = "items")]
		public List<Notification> Items { get; set; }
	}
}
