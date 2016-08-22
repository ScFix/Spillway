using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spillway.Models
{
	[DataContract()]
	public class Mail
	{
		[DataMember(Name = "items")]
		public List<Notification> Items { get; set; }
	}
}