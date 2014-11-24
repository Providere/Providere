using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using System.Web.Security;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class UsuarioRepositorio
    {
        ProvidereEntities entities = new ProvidereEntities();

        //Verifico si el email ingresado ya esta en la base y veo su estado:
        internal Usuario EmailExisteActivado(string email)
        {
            var usuario = (from user in entities.Usuario
                           where user.Mail == email && user.IdEstado == 1  //Si el estado es 1 significa que ya esta activo
                           select user).First();
            return usuario;
        }

        internal Usuario EmailExisteInactivo(string email)
        {
            var usuario = (from user in entities.Usuario
                           where user.Mail == email && user.IdEstado == 4 //Si el estado es 4 significa que esta inactivo
                           select user).First();
            return usuario;
        }

        internal Usuario TraerDatosPorMail(string email)
        {
            var usuario = (from user in entities.Usuario
                           where user.Mail == email
                           select user).First();
            return usuario;
        }

        internal void ModificarUsuario(Usuario user)
        {
            Usuario usuario = entities.Usuario.Where(e => e.Id == user.Id).FirstOrDefault();
            usuario.Nombre = user.Nombre;
            usuario.Apellido = user.Apellido;
            usuario.Contrasenia = user.Contrasenia;
            usuario.Telefono = user.Telefono;
            usuario.Ubicacion = user.Ubicacion;
            usuario.FechaCreacion = DateTime.Now; //se actualiza para mandarle un mail nuevo en caso que hayan pasado los 15' y el estado sea inactivo

            entities.SaveChanges();

        }

        internal void CrearUsuario(Usuario model)
        {
            
            entities.Usuario.AddObject(model);
            entities.SaveChanges();
        }

        internal bool ActivarUsuario(string codAct)
        {
            //Encontrar al propietario del codigo de activacion:
            var user = (from usuarios in entities.Usuario
                        where usuarios.CodActivacion == codAct
                        select usuarios).First();

            Double TiempoPasadoDesdeLaRegistracion = (DateTime.Now - user.FechaCreacion).TotalMinutes;

            // Solo se activa el usuario si la activacion se realiza dentro de los 15 min.
            if (TiempoPasadoDesdeLaRegistracion < 15)
            {

                user.IdEstado = Convert.ToInt16(1); //Pasa a estado activo
                user.FechaCambioEstado = DateTime.Now;
                user.FechaActivacion = DateTime.Now;
                entities.SaveChanges();
                return true;
            }
            else
            {
                //Se vencio el plazo de validez
                return false;
            }
        }

        internal bool UsuarioExistente(Usuario model)
        {
            bool usuarioExiste = entities.Usuario.Any(user => user.Mail == model.Mail && user.Contrasenia == model.Contrasenia);
            return usuarioExiste;
        }

        internal bool UsuarioActivo(Usuario model)
        {
            bool usuarioActivo = entities.Usuario.Any(user => user.Mail == model.Mail && user.IdEstado == 1);
            return usuarioActivo;
        }


        public Usuario ObtenerUsuarioEditar(int idUsuario)
        {
            Usuario usuario = entities.Usuario.Where(e => e.Id == idUsuario).FirstOrDefault();

            return usuario;
        }

        //Modifico los datos del usuario:
        public void ModificarDatosUsuario(int id, string nombre, string apellido, string telefono, string geocomplete2)
        {
            Usuario usuario = entities.Usuario.Where(e => e.Id == id).FirstOrDefault();
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Ubicacion = geocomplete2;
            usuario.Telefono = telefono;

            entities.SaveChanges();
        }

        //Modifico solo la contrasenia:
        public void GuardarContraseniaNueva(int id, string contrasenia)
        {
            Usuario user = entities.Usuario.Where(e => e.Id == id).FirstOrDefault();
            user.Contrasenia = Encryptor.MD5Hash(contrasenia);
            entities.SaveChanges();
        }


        internal void DarDeBajaUsuario(int idUsuario)
        {
            Usuario miUser = entities.Usuario.Where(e => e.Id == idUsuario).FirstOrDefault();
            miUser.IdEstado = 3; //Estado deshabilitado
            entities.SaveChanges();
        }

        //Verificamos si el usuario que intenta registrarse fue dado de baja:
        internal Usuario UsuarioDadoDeBaja(string p)
        {
            var usuario = (from user in entities.Usuario
                           where user.Mail == p && user.IdEstado == 3  //Si el estado es 3 significa que esta deshabilitado
                           select user).First();
            return usuario;
        }

        internal object TraerDatos(string p)
        {
            throw new NotImplementedException();
        }
    }
}