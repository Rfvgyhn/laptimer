using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MongoDB.Bson;

namespace LapTimer.Models.ViewModels
{
    public class EditEventViewModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Participant> Participants { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public ObjectId Id { get; set; }
        public string ShortId { get; set; }
        public IEnumerable<EditEventSessionViewModel> FirstSessionTimes { get; set; }
    }
}