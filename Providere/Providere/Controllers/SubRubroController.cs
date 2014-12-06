using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Providere.Controllers
{
    public class SubRubroController : Controller
    {
        SubRubroServicios subs = new SubRubroServicios();
        // GET: /SubRubro/Lista

        virtual public String Lista(int id)
        {
            List<SubRubroParaJson> subrubros = subs.obtenerPorRubro(id);


            var jsonSerialiser = new JavaScriptSerializer();

            string json = JsonConvert.SerializeObject(subrubros, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

           // return View(jsoneados);
            return json;

        }

    }
}
