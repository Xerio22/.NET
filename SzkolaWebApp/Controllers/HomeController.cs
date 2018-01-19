using System.Web.Mvc;
using SzkolaWebApp.Models;

namespace SzkolaWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult News()
        {
            var model = new NewsViewModel();
            ViewBag.Title = "Aktualności";
            return View(model);
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