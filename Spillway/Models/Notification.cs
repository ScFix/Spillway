using RestSharp.Deserializers;
using Spillway.Interfaces;
using System.Runtime.Serialization;

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