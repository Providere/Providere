using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        PublicacionServicios ps = new PublicacionServicios();
        UsuarioServicios us = new UsuarioServicios();
        SancionServicios ss = new SancionServicios();
        PreguntaServicios prs = new PreguntaServicios();

        public ActionResult Home()
        {
            
            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];

            ViewBag.PreguntaSinResponder = prs.TraerPreguntasSinResponder(Convert.ToInt16(this.Session["IdUsuario"]));

            int limite = 4;

            ViewBag.PublicacionesMasRecientes = ps.traerPublicacionesMasRecientes(limite);

            ViewBag.PrestadoresMejorCalificados = ps.traerPublicacionesMejorCalificadas(limite);

            ViewBag.UsuariosMasCercanos = us.traerPorZona(us.traerUsuario(Convert.ToInt16(this.Session["IdUsuario"])), limite);

            ViewBag.Sancion = ss.ObtenerSancionDeUsuario(us.ObtenerUsuarioEditar(Convert.ToInt16(this.Session["IdUsuario"])));

            //Para verificar si hay mas de 4 y mostrar boton "ver mas"
            ViewBag.PublicacionesMasRecientesMas = ps.traerPublicacionesMasRecientes(5);
            ViewBag.PrestadoresMejorCalificadosMas = ps.traerPublicacionesMejorCalificadas(5);
            ViewBag.UsuariosMasCercanosMas = us.traerPorZona(us.traerUsuario(Convert.ToInt16(this.Session["IdUsuario"])), 5);
            
            return View();
        }

    }
}
