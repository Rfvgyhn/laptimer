using System;
using System.Collections.Generic;
using LapTimer.Data;
using LapTimer.Models;

namespace LapTimer.Services
{
    public class EventService : IEventService
    {
        IRepository repository;

        public EventService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Event> All()
        {
            return repository.All<Event>();
        }

        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            return repository.Find(predicate);
        }

        public Event Single(Func<Event, bool> predicate)
        {
            return repository.Single(predicate);
        }

        public void Save(Event item)
        {
            repository.Save<Event>(item);
        }

        public void Delete(Event item)
        {
            repository.Delete<Event>(item);
        }
    }
}