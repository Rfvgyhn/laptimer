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
        public Dictionary<string, IList<TimeSpan>> Times { get; set; }

        public Session(string name)
        {
            Name = name;
            Times = new Dictionary<string, IList<TimeSpan>>();
        }

        public void AddTime(string key, TimeSpan time)
        {
            if (Times.ContainsKey(key))
                Times[key].Add(time);
            else
                Times.Add(key, new List<TimeSpan> { time });
        }
    }
}