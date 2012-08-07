using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using LapTimer.Models;
using LapTimer.Services;

namespace LapTimer.Controllers
{
    public class HomeController : Controller
    {
        IEventService eventService;

        public HomeController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public ActionResult Index()
        {
            var model = eventService.All().OrderByDescending(e => e.Date);

            return View(model);
        }
    }
}
