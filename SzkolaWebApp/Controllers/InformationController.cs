﻿using System;
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
    }
}