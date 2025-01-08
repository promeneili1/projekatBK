using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomsijaProjekat.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return File(Server.MapPath("~/Views/About/about.html"), "text/html");
        }
    }
}