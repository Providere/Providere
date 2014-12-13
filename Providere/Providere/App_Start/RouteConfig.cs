using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Providere
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               "ActivarUsuario",
               "Cuenta/Activar/{codAct}",
               new { controller = "Usuario", action = "Activar", codAct = UrlParameter.Optional }
           );

            routes.MapRoute(
              "Publicacion",
              "Publicacion/VisualizarPublicacion/{idPublicacion}",
              new { controller = "Publicacion", action = "VisualizarPublicacion", idPublicacion = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}