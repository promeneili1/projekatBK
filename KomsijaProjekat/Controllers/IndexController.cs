using System.Web.Mvc;

namespace ProjekatKomsija.Controllers
{
    public class IndexController : Controller
    {


  
        public ActionResult Index()
        {
            return File(Server.MapPath("~/Views/Index/index.cshtml"), "text/html");
        }
    }
}
