﻿using System;
using System.Collections.Generic;
using LapTimer.Data;
using LapTimer.Models;
using System.Linq;

namespace LapTimer.Services
{
    public class SessionService : ISessionService
    {
        IEventService eventService;

        public SessionService(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public Session Single(object eventKey, string name)
        {
            return Single(eventKey, s => s.Name == name.Trim());
        }

        public Session Single(object eventKey, Func<Session, bool> predicate)
        {
            var @event = eventService.Single(eventKey);

            return @event.Sessions.Where(predicate).Single();
        }

        public void Add(object eventKey, string name)
        {
            var @event = eventService.Single(eventKey);
            var sessionName = name.Trim();

            if (@event.Sessions.Where(s => s.Name == sessionName).Any())
            {
                // validation
            }

            var session = new Session(sessionName);

            @event.Sessions.Add(session);
            eventService.Save(@event);
        }

        public void AddLap(object eventKey, string name, string participantId, double time)
        {
            var @event = eventService.Single(eventKey);
            var session = @event.Sessions.Where(s => s.Name == name.Trim()).Single();

            session.AddTime(participantId, TimeSpan.FromMilliseconds(time));
            eventService.Save(@event);
        }

        public void Update(object eventKey, string name, string newName)
        {
            var @event = eventService.Single(eventKey);
            var session = @event.Sessions.Where(s => s.Name == name.Trim()).SingleOrDefault();

            if (session == null)
            {
                // validation
            }

            session.Name = newName;
            eventService.Save(@event);            
        }

        public void Delete(object eventKey, string name)
        {
            var @event = eventService.Single(eventKey);
            var session = @event.Sessions.Where(s => s.Name == name).SingleOrDefault();

            if (session == null)
            {
                // validation
            }

            @event.Sessions.Remove(session);
            eventService.Save(@event);
        }
    }
}