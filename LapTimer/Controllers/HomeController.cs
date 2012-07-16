using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;

namespace LapTimer.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = "mongodb://localhost/?safe=true";

        public ActionResult Index()
        {
            var server = MongoServer.Create("");
            

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
