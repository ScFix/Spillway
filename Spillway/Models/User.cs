﻿using RestSharp.Deserializers;
using System.Runtime.Serialization;

namespace Spillway.Models
{
    [DataContract()]
    public class User
    {
        [DeserializeAs(Name = "badge_counts")]
        public ReputationBadges Badges { get; set; }

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