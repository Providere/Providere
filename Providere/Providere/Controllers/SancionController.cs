using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;

namespace Providere.Controllers
{
    public class SancionController : Controller
    {
        //
        // GET: /Sancion/DetectarSancionados
        // A principio de cada mes todos los usuarios son evaluados para determinar si son bloqueados o no. 

        public ActionResult DetectarSancionados()
        {
            UsuarioServicios us = new UsuarioServicios();
            us.DetectarSancionados();
            return View();
        }

    }
}
