using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;
using System.Web.Security;

namespace Providere.Servicios
{
    public class UsuarioServicios
    {
        UsuarioRepositorio ur = new UsuarioRepositorio();
        MailServicios mailing = new MailServicios();

        //verificar si ya existe un usuario registrado con ese  email:
        public bool EmailExisteActivado(string email)
        {
            try
            {
                Usuario user = ur.EmailExisteActivado(email);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Verificar si ya existe un usuario registrado pero inactivo con ese email:
        public bool EmailExisteInactivo(string email)
        {
            try
            {
                Usuario user = ur.EmailExisteInactivo(email);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Activar el usuario que esta registrado pero inactivo:
        internal void ActivarUsuarioInactivo(string nombre, string apellido, string mail, string telefono, string contrasenia, string geocomplete)
        {
            var user = ur.TraerDatosPorMail(mail); //Traigo todos los datos de ese usuario que tengo en la BD
            user.Nombre = nombre;
            user.Apellido = apellido;
            user.Contrasenia = Encryptor.MD5Hash(contrasenia);
            user.Telefono = telefono;
            user.Ubicacion = geocomplete;

            //Le envio el mail de confirmacion y actualizo los datos:

            ur.ModificarUsuario(user);
            mailing.EnviarMail(user);
        }

        internal void AgregarUsuarioNuevo(string nombre, string apellido, string mail, string telefono, string contrasenia, string geocomplete)
        {
            Usuario miUsuario = new Usuario();
            miUsuario.Nombre = nombre;
            miUsuario.Apellido = apellido;
            miUsuario.Telefono = telefono;
            miUsuario.Mail = mail;
            miUsuario.Ubicacion = geocomplete;
            miUsuario.Contrasenia = Encryptor.MD5Hash(contrasenia);
            miUsuario.FechaActivacion = DateTime.Now;
            miUsuario.FechaCreacion = DateTime.Now;
            miUsuario.FechaCambioEstado = DateTime.Now;
            miUsuario.IdEstado = Convert.ToInt16(4); //Estado inactivo hasta que confirma su registracion
            miUsuario.CodActivacion = Encryptor.MD5Hash(mail);

            ur.CrearUsuario(miUsuario);
            mailing.EnviarMail(miUsuario);
        }

        //Activar Usuario con confirmacion de email:
        public bool ActivarUsuario(string codAct)
        {

            return ur.ActivarUsuario(codAct);
        }

        //Verifica si existe el usuario:
        public bool UsuarioExistente(Usuario model)
        {
            return ur.UsuarioExistente(model);
        }

        //Verifica si el usuario esta activo:
        public bool UsuarioActivo(Usuario model)
        {
            return ur.UsuarioActivo(model);
        }

        internal object traerIdUsuario(Usuario model)
        {
            Usuario miUsuario = ur.TraerDatosPorMail(model.Mail);
            return miUsuario.Id;
        }


        internal void CrearCookie(Usuario model)
        {
            var user = ur.TraerDatosPorMail(model.Mail);
            FormsAuthentication.SetAuthCookie(user.Nombre, false);
        }

        //Obtener usuario a traves de su id para editar datos:
        internal Usuario ObtenerUsuarioEditar(int idUsuario)
        {
            return ur.ObtenerUsuarioEditar(idUsuario);
        }

        //Modifico datos personales:
        internal void ModificarDatosUsuario(int id, string nombre, string apellido, string telefono, string ubicacion)
        {
            ur.ModificarDatosUsuario(id, nombre, apellido, telefono, ubicacion);
        }

        //Modifico contraseña solo si el usuario desea cambiarla:
        internal void GuardarContraseniaNueva(int id, string contrasenia)
        {
            ur.GuardarContraseniaNueva(id, contrasenia);
        }

        //Damos de baja al usuario:
        internal void DarDeBajaUsuario(int idUsuario)
        {
            ur.DarDeBajaUsuario(idUsuario);
        }

        //Verificamos si fue dado de baja:
        public bool UsuarioDadoDeBaja(string p)
        {
            try
            {
                Usuario usuario = ur.UsuarioDadoDeBaja(p);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}