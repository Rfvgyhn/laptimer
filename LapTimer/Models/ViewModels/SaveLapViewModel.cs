using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Models.ViewModels
{
    public class SaveLapViewModel
    {
        public int Lap { get; set; }
        public float Time { get; set; }
        public string EventId { get; set; }
        public string SessionName { get; set; }
        public string Participant { get; set; }
    }
}