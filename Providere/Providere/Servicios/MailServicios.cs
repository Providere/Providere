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
            msj.From = new MailAddress("unlampw3@gmail.com");
            msj.Subject = "Activación de cuenta";
            msj.SubjectEncoding = System.Text.Encoding.UTF8;
            string body = "Hola " + model.Nombre.Trim() + ",";
            body += "<br/><br/>Por favor, hace click en el siguiente link para activar tu cuenta:<br/>";
            body += "<br /><a href='"
                 + HttpContext.Current.Request.Url.Scheme
                 + "://"
                 + HttpContext.Current.Request.Url.Authority + "/Cuenta/Activar/"
                 + model.CodActivacion
                 + "'>Hace click aqui para activar tu cuenta</a>";
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

        public void EnviarMailContrasenia(Usuario model)
        {
            //ENVIO DE MAIL:
            msj.To.Add(new MailAddress(model.Mail));
            msj.From = new MailAddress("unlampw3@gmail.com");
            msj.Subject = "Recupera tu contraseña de Providere";
            msj.SubjectEncoding = System.Text.Encoding.UTF8;
            string body = "Hola " + model.Mail.Trim() + ",";
            body += "<br/><br/>Por favor, hace click en el siguiente link para cambiar tu contraseña:<br/>";
            body += "<br /><a href='"
                 + HttpContext.Current.Request.Url.Scheme
                 + "://"
                 + HttpContext.Current.Request.Url.Authority + "/Cuenta/NuevaContrasenia/"
                   + model.CodActivacion
                 + "'>Hace click aqui para cambiar tu contraseña</a>";
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

        //Mail para notificar al prestador que un cliente hizo una pregunta en su publicacion:
        public void EnviarMailPregunta(Publicacion publicacion)
        {
            msj.To.Add(new MailAddress(publicacion.Usuario.Mail));
            msj.From = new MailAddress("unlampw3@gmail.com");
            msj.Subject = "Nueva pregunta en tu publicacion";
            msj.SubjectEncoding = System.Text.Encoding.UTF8;
            string body = "Hola " + publicacion.Usuario.Nombre.Trim() + ",";
            body += "<br/><br/>Tenes una nueva pregunta en tu publicación:<br/>";
            body += publicacion.Titulo;
            body += "<br /><a href='"
                + HttpContext.Current.Request.Url.Scheme
                + "://"
                + HttpContext.Current.Request.Url.Authority + "/Publicacion/VisualizarPublicacion/"
                  + publicacion.Id
                + "'>Hace click aqui para ver la publicacion</a>";
            body += "<br /><br />El equipo de Providere!";
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