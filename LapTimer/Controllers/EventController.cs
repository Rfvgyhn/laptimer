using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTimer.Services;
using LapTimer.Models;
using LapTimer.Models.ViewModels;
using LapTimer.Infrastructure.Extensions;
using MongoDB.Bson;

namespace LapTimer.Controllers
{
    public class EventController : Controller
    {
        IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ByDate(string slug, DateTime? date = null)
        {
            if (date != null)
            {
                var @event = eventService.Single(e => e.Location.Slug == slug && e.Date.Date == date.Value.Date);

                return View("Details", @event);
            }
            
            var model = eventService.Find(e => e.Location.Slug == slug);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(CreateEventViewModel model)
        {
            Event e = new Event
            {
                Location = new Location { Name = model.LocationName, Slug = model.LocationName.ToSlug() },
                Date = DateTime.UtcNow
            };
            Session session = new Session("Session 1");

            foreach (var kvp in model.Participants)
                session.Participants.Add(new Participant { Name = kvp.Value, Number = kvp.Key });

            e.Sessions.Add(session);

            eventService.Save(e);

            return RedirectToRoute("ByDate", new { slug = e.Location.Slug, date = e.Date.ToSlug(), action = "ByDate" });
            
        }

        public JsonResult GetTimes(string eventId, string sessionName)
        {
            var @event = eventService.Single(eventId);
            var session = @event.Sessions.Where(s => s.Name == sessionName).Single();
            var result = session.Participants
                                .Select(p => new
                                {
                                    name = p.Name,
                                    number = p.Number,
                                    times = p.Times.Select(t => t.TotalMilliseconds)
                                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string slug, DateTime date)
        {
            var model = eventService.Single(e => e.Location.Slug == slug && e.Date.Date == date.Date);

            return View(model);
        }
        
        [HttpPost]
        public JsonResult AddLap(SaveLapViewModel model)
        {
            var @event = eventService.Single(model.EventId);
            var session = @event.Sessions.Where(s => s.Name == model.SessionName).Single();
            var participant = session.Participants.Where(p => p.Number == model.Participant).Single();

            participant.Times.Add(TimeSpan.FromMilliseconds(model.Time));

            eventService.Save(@event);

            return Json(new { });
        }

        [HttpPost]
        public JsonResult AddSession(string eventId, string name)
        {
            var @event = eventService.Single(eventId);
            var session = new Models.Session(name);

            foreach (var p in @event.Sessions.First().Participants)
                session.Participants.Add(new Participant { Number = p.Number, Name = p.Name });

            @event.Sessions.Add(session);
            
            eventService.Save(@event);

            return Json(new { });
        }

        [HttpPost]
        public JsonResult EditSession(string eventId, string name, string newName)
        {
            var @event = eventService.Single(eventId);
            var session = @event.Sessions.Where(s => s.Name == name).Single();

            session.Name = newName;
            eventService.Save(@event);

            return Json(new { });
        }

        [HttpPost]
        public JsonResult DeleteSession(string eventId, string name)
        {
            var @event = eventService.Single(eventId);
            var session = @event.Sessions.Where(s => s.Name == name).Single();

            @event.Sessions.Remove(session);
            eventService.Save(@event);

            return Json(new { });
        }
    }
}
