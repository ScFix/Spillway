using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Models
{
	[DataContract]
	public class Users
	{
		[DataMember(Name = "items")]
		public List<User> Items { get; set; }
	}
}
