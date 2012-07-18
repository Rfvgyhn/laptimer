using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Models
{
    public class Session
    {
        public IList<Participant> Participants { get; set; }
        public int Id { get; set; }

        public Session()
        {
            Participants = new List<Participant>();
        }
    }
}