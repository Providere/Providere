using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;

namespace Providere.Controllers
{
    public class CalificacionController : Controller
    {

        CalificacionServicios cs = new CalificacionServicios();
        ContratacionServicios crs = new ContratacionServicios();
        
        public ActionResult CalificarUsuario()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CalificarUsuario(int idContratacion)
        {
            int idCalificador = Int32.Parse(this.Session["IdUsuario"].ToString());
            int idTipoEvaluacion = Int32.Parse(Request["idTipoEvaluacion"]);
            int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);
            cs.calificarUsuario(idContratacion, idTipoEvaluacion, idTipoCalificacion);

            ViewBag.Mensaje = "La publicacion se ha calificado con éxito";

            return View("Index","Contratacion");

        }
    }
}
