using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spillway.Models
{
	[DataContract]
	public class Users
	{
		[DataMember(Name = "items")]
		public List<User> Items { get; set; }
	}
}