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
        internal void ActivarUsuarioInactivo(Usuario model)
        {
            var user = ur.TraerDatosPorMail(model.Mail); //Traigo todos los datos de ese usuario que tengo en la BD
            user.Nombre = model.Nombre;
            user.Apellido = model.Apellido;
            user.Contrasenia = Encryptor.MD5Hash(model.Contrasenia);
            user.Telefono = model.Telefono;
            user.Ubicacion = model.Ubicacion;

            //Le envio el mail de confirmacion y actualizo los datos:
           
            ur.ModificarUsuario(user);
            mailing.EnviarMail(user);
        }

        internal void AgregarUsuarioNuevo(Usuario model)
        {

            ur.CrearUsuario(model);
            mailing.EnviarMail(model);
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

        //Verifica que el usuario nunca se registro:
        internal bool UsuarioInexistente(Usuario model)
        {
            return ur.UsuarioInexistente(model);
        }

        //Verifica si el usuario esta activo:
        public bool UsuarioActivo(Usuario model)
        {
            return ur.UsuarioActivo(model);
        }

        internal object traerIdUsuario(Usuario model)
        {
            Usuario miUsuario = ur.traerDatosPorMail(model.Mail);
            return miUsuario.Id;
        }


        internal void CrearCookie(Usuario model)
        {
            FormsAuthentication.SetAuthCookie(model.Mail, false);
        }

        //Obtener usuario a traves de su id para editar datos
        internal Usuario ObtenerUsuarioEditar(int idUsuario)
        {
            return ur.ObtenerUsuarioEditar(idUsuario);
        }
    }
}