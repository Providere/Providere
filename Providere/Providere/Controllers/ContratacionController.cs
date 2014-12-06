using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;

namespace Providere.Controllers
{
    public class ContratacionController : Controller
    {
        //
        // GET: /Contratacion/
        ContratacionServicios cs = new ContratacionServicios();
        public ActionResult Index()
        {

            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var contrataciones = cs.traerContratacionesRealizadas(idUsuario);

            return View(contrataciones);
        }

    }
}
