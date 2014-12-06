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
        
        public ActionResult CalificarUsuario(int idContratacion)
        {
           
            var con = crs.traerPorId(idContratacion);
            return View(con);

        }

        [HttpPost]
        public ActionResult CalificarUsuario()
        {
            int idCalificador = Int32.Parse(this.Session["IdUsuario"].ToString());
            int idContratacion = Int32.Parse(Request["idContratacion"]);
            int idTipoEvaluacion = Int32.Parse(Request["idTipoEvaluacion"]);
            int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);
            cs.calificarUsuario(idContratacion, idTipoEvaluacion, idTipoCalificacion);

            ViewBag.mensaje = "La publicacion se ha calificado con éxito.";

            return View("Index","Contratacion");

        }
    }
}
