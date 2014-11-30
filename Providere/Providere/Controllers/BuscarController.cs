using System;
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

        BuscarServicios ps = new BuscarServicios();
        ProvidereEntities context = new ProvidereEntities();

        public ActionResult Resultados(Rubro Rubro, SubRubro SubRubro, String Ubicacion)
        {            
            var publicaciones = ps.buscar(Rubro,SubRubro,Ubicacion); 
            return View(publicaciones.ToList());
        }

    }
}
