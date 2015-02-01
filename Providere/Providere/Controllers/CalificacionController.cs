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

        public ActionResult CalificarUsuario(int idContratacion, int idTipoCalificacion)
        {
            ViewBag.IdContratacion = idContratacion;
            ViewBag.IdTipoCalificacion = idTipoCalificacion;

            return View();
        }

        [HttpPost]
        public ActionResult CalificarUsuario()
        {

            // int idCalificador = Int32.Parse(this.Session["IdUsuario"].ToString());
            // int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);

            int idContratacion = Int32.Parse(Request["IdContratacion"]);

            int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);

            int idTipoEvaluacion = Int32.Parse(Request["calificacion"]);
                
            string comentario = Convert.ToString(Request["Descripcion"]);

            cs.calificarUsuario(idContratacion, idTipoEvaluacion, idTipoCalificacion, comentario);

            ViewBag.Mensaje = "El usuario se ha calificado con éxito";

            return View("Index", "Contratacion");

        }
    }
}
