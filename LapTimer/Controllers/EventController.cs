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
using LapTimer.Infrastructure;

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

        public ActionResult BySlug(string slug, int? year, int? month, int? day)
        {            
            var model = eventService.FindBySlug(slug, year, month, day);

            return View(model);
        }

        public ActionResult Details(string id, string slug)
        {
            var @event = eventService.SingleByShortKey(id);

            if (@event.Location.Slug != slug)
                return RedirectPermanent(Url.RouteUrl("Event", new { id = id, slug = @event.Location.Slug }));

            return View("Details", @event);
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

            return RedirectToAction("Details", new { id = e.ShortId, slug = e.Location.Slug });
            
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

        public ActionResult Edit(string id)
        {
            var model = eventService.Single(id);

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
