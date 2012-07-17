using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LapTimer.Models;
using MongoDB.Driver;
using LapTimer.Data;

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