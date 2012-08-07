using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MongoDB.Bson;

namespace LapTimer.Models.ViewModels
{
    public class EditEventSessionViewModel
    {
        public string DisplayNumber { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int Lap { get; set; }
        public TimeSpan LastLap { get; set; }
    }
}