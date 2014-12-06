using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Providere.Servicios;

namespace Providere
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalFilters.Filters.Add(new RubrosParaSelect(), 0);

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            if (Session["IdUsuario"] != null)
            {
                //Redirect to Welcome Page if Session is not null  
                Response.Redirect("Home/Index");

            }
            else
            {
                //Redirect to Login Page if Session is null & Expires   
                Response.Redirect("Index/Index");

            }


        }  
    }
}