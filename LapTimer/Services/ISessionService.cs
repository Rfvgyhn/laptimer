using System;
using System.Collections.Generic;
using LapTimer.Models;

namespace LapTimer.Services
{
    public interface ISessionService
    {
        Session Single(object eventKey, string name);
        Session Single(object eventKey, Func<Session, bool> predicate);
        void Add(object eventKey, string name);
        void Update(object eventKey, string name, string newName);
        void Delete(object eventKey, string name);
        void AddLap(object eventKey, string name, string participantId, double time);
    }
}
