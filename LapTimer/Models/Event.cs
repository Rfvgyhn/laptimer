using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace LapTimer.Models
{
    public class Event
    {
        public ObjectId Id { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Participant> Participants { get; set; }

        public Event()
        {
            Sessions = new List<Session>();
            Participants = new List<Participant>();
        }
    }
}