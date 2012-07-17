using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapTimer.Models;

namespace LapTimer.Services
{
    public interface IEventService
    {
        IEnumerable<Event> All();
        void Save(Event item);
        void Delete(Event item);
    }
}
