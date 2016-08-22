using RestSharp.Deserializers;
using System.Runtime.Serialization;

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
        public int Gold { get; set; }
    }
}