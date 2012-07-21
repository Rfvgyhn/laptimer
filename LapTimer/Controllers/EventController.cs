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
        ISessionService sessionService;

        public EventController(IEventService eventService, ISessionService sessionService)
        {
            this.eventService = eventService;
            this.sessionService = sessionService;
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
            var session = sessionService.Single(eventId, sessionName);
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
            sessionService.AddLap(model.EventId, model.SessionName, model.Participant, model.Time);

            return Json(new { });
        }

        [HttpPost]
        public JsonResult AddSession(string eventId, string name)
        {                       
            sessionService.Add(eventId, name);

            return Json(new { });
        }

        [HttpPost]
        public JsonResult EditSession(string eventId, string name, string newName)
        {
            sessionService.Update(eventId, name, newName);

            return Json(new { });
        }

        [HttpPost]
        public JsonResult DeleteSession(string eventId, string name)
        {
            sessionService.Delete(eventId, name);

            return Json(new { });
        }
    }
}
