using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KomsijaProjekat.Models;

namespace KomsijaProjekat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*htmlfile}", new { htmlfile = @".*\.html" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "About", action = "Index" }
            );

            routes.MapRoute(
                name: "User",
                url: "user",
                defaults: new { controller = "User", action = "User" }
            );

            routes.MapRoute(
                name: "Car",
                url: "car",
                defaults: new { controller = "Car", action = "Car" }
            );
            routes.MapRoute(
                name: "Product",
                url: "product",
                defaults: new { controller = "Product", action = "Product" }
            );

            routes.MapRoute(
                name: "Brand",
                url: "brand",
                defaults: new { controller = "Brand", action = "Brand" }
            );



            routes.MapRoute(
                name: "Delete",
                url: "User/Delete/{id}",
                defaults: new { controller = "User", action = "Delete" }
);
            routes.MapRoute(
           name: "CRUD",
           url: "{controller}/{action}/{id}",
           defaults: new { action = "Index", id = UrlParameter.Optional }
       );


        }

    }
}
