using System;
using System.Collections.Generic;
using LapTimer.Models;

namespace LapTimer.Services
{
    public interface IEventService
    {
        IEnumerable<Event> All();
        IEnumerable<Event> FindBySlug(string slug, int? year, int? month, int? day);
        IEnumerable<Event> Find(Func<Event, bool> predicate);
        Event Single(object key);
        Event Single(Func<Event, bool> predicate);
        Event SingleByShortKey(string shortKey);
        void Save(Event item);
        void Delete(Event item);
    }
}
