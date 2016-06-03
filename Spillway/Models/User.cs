using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Spillway.Models
{
	[DataContract()]
	public class User
	{
		[DeserializeAs(Name = "reputation")]
		public int Reputation
		{
			get; set;
		}

		[DeserializeAs(Name = "user_id")]
		public int Id
		{
			get; set;
		}

		[DeserializeAs(Name = "display_name")]
		public string Name
		{
			get; set;
		}

		[DeserializeAs(Name = "profile_image")]
		public string ImageUrl { get; set; }

	}
}
