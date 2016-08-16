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
	public class ReputationBadges
	{
		[DeserializeAs(Name = "bronze")]
		public int Bronze { get; set; }
		[DeserializeAs(Name = "silver")]
		public int Silver { get; set; }
		[DeserializeAs(Name = "gold")]
		public int Gold{ get; set; }

	}
}
