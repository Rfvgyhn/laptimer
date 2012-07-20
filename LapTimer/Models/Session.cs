using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace LapTimer.Models
{
    public class Session
    {
        [BsonId]
        public string Name { get; set; }
        public IList<Participant> Participants { get; set; }

        public Session(string name)
        {
            Name = name;
            Participants = new List<Participant>();
        }
    }
}