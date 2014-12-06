using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Servicios;
using System.Web.Mvc;

namespace Providere.Servicios
{
    public class RubrosParaSelect : ActionFilterAttribute
    {

        RubroServicios rs = new RubroServicios();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
           filterContext.Controller.ViewBag.Rubros = rs.obtenerTodos();         
        }

    }
}