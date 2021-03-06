﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using LapTimer.Services;
using LapTimer.Data;
using System.Web.Configuration;
using LapTimer.Infrastructure;

namespace LapTimer
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Event",
                "event/{action}/{id}/{slug}",
                new { controller = "Event", action = "Details", slug = UrlParameter.Optional },
                new { id = @"[A-Za-z0-9]{16}" }
            );

            routes.MapRoute(
                "BySlug",
                "events/{slug}/{year}/{month}/{day}",
                new { controller = "Event", action = "BySlug", year = UrlParameter.Optional, month = UrlParameter.Optional, day = UrlParameter.Optional },
                new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { action = @"^[^0-9].+" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new DefaultDictionaryBinder();

            var assembly = typeof(MvcApplication).Assembly;
            var builder = new ContainerBuilder();            
            builder.RegisterControllers(assembly);
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            builder.RegisterType<MongoRepository>().As<IRepository>()
                   .WithParameter(new NamedParameter("connectionString", WebConfigurationManager.AppSettings.Get("MONGOLAB_URI"))); ;            

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}