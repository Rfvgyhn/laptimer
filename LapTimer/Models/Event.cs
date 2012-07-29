using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using LapTimer.Infrastructure;

namespace LapTimer.Models
{
    public class Event
    {
        public ObjectId Id { get; set; }
        public Location Location { get; set; }

        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public IList<Session> Sessions { get; set; }

        public Event()
        {
            Sessions = new List<Session>();
        }

        public string ShortId
        {
            get 
            { 
                return Convert.ToBase64String(Id.ToByteArray());
            }
        }
    }
}