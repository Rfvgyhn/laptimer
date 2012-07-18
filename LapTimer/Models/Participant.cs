using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Models
{
    public class Participant
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public IList<TimeSpan> Times { get; set; }

        public Participant()
        {
            Times = new List<TimeSpan>();
        }
    }
}