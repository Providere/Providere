using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using context = System.Web.HttpContext;


namespace Providere
{
    public class ClientException:ApplicationException
    {
        public ClientException(String mensaje, Exception ex)
            : base(mensaje)
        {
            LogException(ex, mensaje);
        }

        // Log an Exception
        public static string LogException(Exception exc, string source)
        {
            try
            {
                string path = context.Current.Server.MapPath("~/Errores/");
                // chequea el directorio
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + DateTime.Today.ToString("dd-MMM-yy") + ".log";
                // chequea el archivo
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                // log del error
                using (StreamWriter writer = File.AppendText(path))
                {
                    string error = "\r\n Fecha del Log : " + DateTime.Now.ToString() +
                    "\r\n Error ocurrido en la pagina : " + context.Current.Request.Url.ToString() +
                    "\r\n\r\n Descripcion :\n" + exc.ToString();
                    writer.WriteLine(error);
                    writer.WriteLine("\r\n<===========================================================================>");
                    writer.Flush();
                    writer.Close();
                }
                return source;
            }
            catch
            {
                return source;
            }
        }
    }
}