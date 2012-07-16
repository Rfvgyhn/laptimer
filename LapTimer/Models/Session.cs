using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Models
{
    public class Session
    {
        public IList<Time> Times { get; set; }

        public Session()
        {
            Times = new List<Time>();
        }
    }
}