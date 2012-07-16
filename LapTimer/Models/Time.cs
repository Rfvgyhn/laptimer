using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Models
{
    public class Time
    {
        public Participant Participant { get; set; }
        public IList<TimeSpan> Splits { get; set; }

        public Time()
        {
            Splits = new List<TimeSpan>();
        }
    }
}