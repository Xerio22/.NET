using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SzkolaWebApp.Controllers
{
    public class InformationController : Controller
    {
        // GET: Informations
        public ActionResult Information()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult YearCalendar()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult Events()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult LessonSchedules()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult LessonsScheduleYoungerClasses()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult LessonsScheduleOlderClasses()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult BellsSchedule()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult Employees()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult BusSchedule()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult Meetings()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult Holidays()
        {
            ViewBag.Title = "Informacje";
            return View();
        }

        public ActionResult Educators()
        {
            ViewBag.Title = "Informacje";
            return View();
        }
    }
}