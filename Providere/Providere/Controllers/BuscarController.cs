﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    public class BuscarController : Controller
    {
        //
        // GET: /Buscar/Resultados

        BuscarServicios bs = new BuscarServicios();
        PublicacionServicios ps = new PublicacionServicios();
        RubroServicios rs = new RubroServicios();
        SubRubroServicios ss = new SubRubroServicios();
        ProvidereEntities context = new ProvidereEntities();

        [HttpPost]
        public ActionResult Resultados()
        {
            var publicaciones = bs.buscar(Request["selectRubro"], Request["IdSubRubro"], Request["geocomplete"]);

            if (Request["selectRubro"] != null)
            {
                ViewBag.rubroElegido = rs.traerDatosPorId(Int32.Parse(Request["selectRubro"]));
            }
            else
            {
                ViewBag.rubroElegido = null;
            }

            if (Request["IdSubRubro"] != null)
            {
                ViewBag.subRubroElegido = ss.traerDatosPorId(Int32.Parse(Request["IdSubRubro"]));
            }
            else
            {
                ViewBag.subRubroElegido = null;
            }

            ViewBag.ubicacionElegida = Request["geocomplete"];

            publicaciones = ps.traerPublicacionesMasRecientes(8);

                return View(publicaciones);
        }

    }
}
