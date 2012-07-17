using System;
using System.Collections.Generic;
using LapTimer.Models;

namespace LapTimer.Services
{
    public interface IEventService
    {
        IEnumerable<Event> All();
        IEnumerable<Event> Find(Func<Event, bool> predicate);
        Event Single(Func<Event, bool> predicate);
        void Save(Event item);
        void Delete(Event item);
    }
}
