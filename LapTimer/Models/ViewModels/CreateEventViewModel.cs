using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace LapTimer.Models.ViewModels
{
    public class CreateEventViewModel
    {
        [DisplayName("Name")]
        public string LocationName { get; set; }

        public List<KeyValuePair<string, string>> Participants { get; set; }

        public CreateEventViewModel()
        {
            Participants = new List<KeyValuePair<string, string>>();
        }
    }
}