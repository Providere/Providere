using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    public class CalificacionController : Controller
    {

        CalificacionServicios cs = new CalificacionServicios();
        ContratacionServicios crs = new ContratacionServicios();


        public ActionResult Index ()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            int limite = 4;
            var obtenidas = cs.TraerCalificacionesObtenidas(idUsuario,limite); //Soy calificado
            var otorgadas = cs.TraerCalificacionesOtorgadas(idUsuario,limite); //Soy calificador
            ViewBag.Obtenidas = obtenidas; 
            ViewBag.Otorgadas = otorgadas;

            var obtenidasTodas = cs.TraerCalificacionesObtenidasTodas(idUsuario); 
            var otorgadasTodas = cs.TraerCalificacionesOtorgadasTodas(idUsuario); 
            ViewBag.ObtenidasTodas = obtenidasTodas;
            ViewBag.OtorgadasTodas = otorgadasTodas;

            return View();
        }

        public ActionResult CalificarUsuario(int idContratacion, int idTipoCalificacion)
        {
            if (idTipoCalificacion == 1)
                ViewBag.Texto = "por el servicio brindado";
            else
                ViewBag.Texto = "por la contratación de";

            var traigoContratacion = crs.traerPorId(idContratacion);

            ViewBag.Contratacion = traigoContratacion;

           // ViewBag.IdContratacion = idContratacion;
            ViewBag.IdTipoCalificacion = idTipoCalificacion;

            return View();
        }

        [HttpPost]
        public ActionResult CalificarUsuario()
        {

            int idContratacion = Int32.Parse(Request["idContratacion"]);

            int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);

            int idTipoEvaluacion = Int32.Parse(Request["IdTipoEvaluacion"]);
                
            string comentario = Convert.ToString(Request["Descripcion"]);

            if (ModelState.IsValid)
            {
                cs.calificarUsuario(idContratacion, idTipoEvaluacion, idTipoCalificacion, comentario);

                TempData["Exito"] = "El usuario se ha calificado con éxito";

                return RedirectToAction("Index", "Contratacion");
            }
            else
            {
                TempData["Error"] = "No se pudo guardar la calificación, intentalo nuevamente";
                return RedirectToAction("Index", "Contratacion");
            }
        }
    }
}
