using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace LapTimer.Models.ViewModels
{
    public class SaveLapViewModel
    {
        public int Lap { get; set; }
        public float Time { get; set; }
        public string EventId { get; set; }
        public int SessionId { get; set; }
        public int Participant { get; set; }
    }
}