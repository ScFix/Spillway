using RestSharp.Deserializers;
using Spillway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Models
{
	[DataContract()]
	public class Notification : INotification
	{
		[DeserializeAs(Name = "is_unread")]
		public int IsUnread
		{
			get; set;
		}

		[DeserializeAs(Name = "creation_date")]
		public long Date
		{
			get; set;
		}

		[DeserializeAs(Name = "item_type")]
		public string Type
		{
			get; set;
		}

		[DeserializeAs(Name = "link")]
		public string Link { get; set; }

	}
}
