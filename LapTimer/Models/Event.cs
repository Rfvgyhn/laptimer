using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using LapTimer.Infrastructure.Extensions;

namespace LapTimer.Models
{
    public class Event
    {
        public ObjectId Id { get; set; }
        public Location Location { get; set; }

        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Participant> Participants { get; private set; }

        public Event()
        {
            Sessions = new List<Session>();
            Participants = new List<Participant>();
        }

        public void AddParticipant(Participant participant)
        {
            var exsiting = Participants.Where(p => p.Number == participant.Number);
            if (exsiting.Any())
                participant.Number += "-" + (exsiting.Count() + 1);

            Participants.Add(participant);
        }

        public void AddParticipants(IEnumerable<Participant> participants)
        {
            foreach (var p in participants)
                AddParticipant(p);
        }

        public string ShortId
        {
            get 
            {
                return Id.ToString().BaseConvert(16, 62);
            }
        }
    }
}