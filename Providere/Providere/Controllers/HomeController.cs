﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Providere.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public ActionResult Home()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            return View();
        }

    }
}
