﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    public class ContratacionController : Controller
    {
        //
        // GET: /Contratacion/
        ContratacionServicios cs = new ContratacionServicios();
        UsuarioServicios us = new UsuarioServicios();
        CalificacionServicios cas = new CalificacionServicios();

        public ActionResult Index()
        {
            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);

            var contrataciones = cs.traerContratacionesRealizadas(idUsuario);

            var quienMeContrato = cs.traerQuienesMeContrataron(idUsuario);

            var calificacionesAprestador = cas.TraerCalificacionPorContratacion(contrataciones, 1);
            var calificacionesACliente = cas.TraerCalificacionPorContratacion(quienMeContrato, 2);

            ViewBag.ContratacionesRealizadas = contrataciones;
            ViewBag.QuienesMeContrataron = quienMeContrato;

            ViewBag.calificacionesAprestador = calificacionesAprestador;
            ViewBag.calificacionesACliente = calificacionesACliente;

            return View();
        }

        /*[ActionName("Index")] public ActionResult SegundaSolapa()
        {
            ViewBag.Error = TempData["Error"];
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var quienMeContrato = cs.traerQuienesMeContrataron(idUsuario);

            return View(quienMeContrato);
        }*/

        public ActionResult Contratar(Publicacion publicacion)
        {

            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var usuario = us.ObtenerUsuarioEditar(idUsuario);
            if (publicacion.IdUsuario == idUsuario) //Significa que el usuario que publico es el mismo que inicio sesion
            {
                TempData["Error"] = "No podes contratar tu publicación.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    var contratacion = cs.nuevaContratacion(publicacion, usuario);
                    TempData["Exito"] = "Contratación exitosa.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al contratar la publicación.");
                    return RedirectToAction("Error", "Shared");
                }
            }
        }

    }
}
