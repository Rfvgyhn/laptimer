using System;
using System.Collections.Generic;
using LapTimer.Data;
using LapTimer.Models;
using System.Linq;
using LapTimer.Infrastructure.Extensions;

namespace LapTimer.Services
{
    public class EventService : IEventService
    {
        IRepository repository;

        public EventService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Event> FindBySlug(string slug, int? year = null, int? month = null, int? day = null)
        {
            string dayCheck = day.HasValue ? day.ToString() : "01";
            string monthCheck = month.HasValue ? month.ToString() : "01";
            DateTime dateCheck;

            if (year.HasValue && !DateTime.TryParse("{0}-{1}-{2}".FormatWith(year.ToString(), monthCheck, dayCheck), out dateCheck))
                return Enumerable.Empty<Event>();

            Func<Event, bool> predicate;

            if (day.HasValue)
                predicate = e => e.Location.Slug == slug && e.Date.Date == new DateTime(year.Value, month.Value, day.Value).Date;
            else if (month.HasValue)
                predicate = e => e.Location.Slug == slug && e.Date.Month == month && e.Date.Year == year;
            else if (year.HasValue)
                predicate = e => e.Location.Slug == slug && e.Date.Year == year;
            else
                predicate = e => e.Location.Slug == slug;

            return Find(predicate);
        }

        public IEnumerable<Event> All()
        {
            return repository.All<Event>();
        }

        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            return repository.Find(predicate);
        }

        public Event Single(object key)
        {
            return repository.Single<Event>(key);
        }

        public Event Single(Func<Event, bool> predicate)
        {
            return repository.Single(predicate);
        }

        public Event SingleByShortKey(string shortKey)
        {
            var key = Convert.FromBase64String(shortKey);
            return repository.Single<Event>(key);
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