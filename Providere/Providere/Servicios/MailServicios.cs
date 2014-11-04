using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Providere.Models;
using System.Net;

namespace Providere.Servicios
{
    public class MailServicios
    {
        System.Net.Mail.MailMessage msj = new System.Net.Mail.MailMessage();

        SmtpClient client = new SmtpClient();

        // El usuario recibirá un email de activación que contendrá el link donde se activará su usuario registrado:
        public void EnviarMail(Usuario model)
        {
            //ENVIO DE MAIL:
            msj.To.Add(new MailAddress(model.Mail));
            msj.From = new MailAddress("nuestra.aplicacion2014@gmail.com");
            msj.Subject = "Activación de cuenta";
            msj.SubjectEncoding = System.Text.Encoding.UTF8;
            string body = "Hola " + model.Nombre.Trim() + ",";
            body += "<br/><br/>Por favor, haga click en el siguiente link para activar su cuenta:<br/>";
            body += "<br /><a href='"
                 + HttpContext.Current.Request.Url.Scheme
                 + "://"
                 + HttpContext.Current.Request.Url.Authority + "/Cuenta/Activar/"
                 + model.CodActivacion
                 + "'>Haga click aqui para activar su cuenta</a>";
            body += "<br /><br />Muchas gracias, el equipo de Providere!";
            msj.Body = body;
            msj.IsBodyHtml = true;

            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            NetworkCredential netcred = new NetworkCredential("unlampw3", "unlampw1c-2014");
            client.UseDefaultCredentials = true;
            client.Credentials = netcred;
            client.Port = 587;
            client.Send(msj);
        }
    }
}