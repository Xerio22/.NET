using System.Web.Mvc;

namespace SzkolaWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult News()
        {
            ViewBag.Title = "Aktualności";
            return View(); // pass title
        }

        public ActionResult History()
        {
            ViewBag.Title = "Historia szkoły";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Kontakt";
            return View();
        }
    }
}