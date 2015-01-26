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
        ProvidereEntities context = new ProvidereEntities();

        //Verifico si el email o el DNI ingresado ya esta en la base y veo su estado:
        public Usuario UsuarioExisteActivado(string email, string dni)
        {
            var usuario = (from user in context.Usuario
                           where (user.Mail == email && user.IdEstado == 1) || (user.Dni == dni && user.IdEstado == 1)  //Si el estado es 1 significa que ya esta activo
                           select user).First();
            return usuario;
        }

        public Usuario UsuarioExisteInactivo(string email, string dni)
        {
            var usuario = (from user in context.Usuario
                           where (user.Mail == email && user.IdEstado == 4) || (user.Dni == dni && user.IdEstado == 4)  //Si el estado es 4 significa que esta inactivo
                           select user).First();
            return usuario;
        }

        public Usuario TraerDatosPorMailDni(string email, string dni)
        {
            var usuario = (from user in context.Usuario
                           where (user.Mail == email) || (user.Dni == dni)
                           select user).First();
            return usuario;
        }

        public void ModificarUsuario(Usuario user)
        {
            Usuario usuario = context.Usuario.Where(e => e.Id == user.Id).FirstOrDefault();
            usuario.Nombre = user.Nombre;
            usuario.Apellido = user.Apellido;
            usuario.Dni = user.Dni;
            usuario.Mail = user.Mail;
            usuario.Contrasenia = user.Contrasenia;
            usuario.Telefono = user.Telefono;
            usuario.Ubicacion = user.Ubicacion;
            usuario.FechaCreacion = DateTime.Now; //se actualiza para mandarle un mail nuevo en caso que hayan pasado los 15' y el estado sea inactivo

            context.SaveChanges();

        }

        public void CrearUsuario(Usuario model)
        {
            
            context.Usuario.AddObject(model);
            context.SaveChanges();
        }

        public bool ActivarUsuario(string codAct)
        {
            //Encontrar al propietario del codigo de activacion:
            var user = (from usuarios in context.Usuario
                        where usuarios.CodActivacion == codAct
                        select usuarios).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {

                Double TiempoPasadoDesdeLaRegistracion = (DateTime.Now - user.FechaCreacion).TotalMinutes;

                // Solo se activa el usuario si la activacion se realiza dentro de los 15 min.
                if (TiempoPasadoDesdeLaRegistracion < 15)
                {

                    user.IdEstado = 1; //Pasa a estado activo
                    user.FechaCambioEstado = DateTime.Now;
                    user.FechaActivacion = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    //Se vencio el plazo de validez
                    return false;
                }
            }
        }

        public bool UsuarioExistente(Usuario model)
        {
            bool usuarioExiste = context.Usuario.Any(user => user.Mail == model.Mail && user.Contrasenia == model.Contrasenia);
            return usuarioExiste;
        }

        public bool UsuarioActivo(Usuario model)
        {
            bool usuarioActivo = context.Usuario.Any(user => user.Mail == model.Mail && user.IdEstado == 1);
            return usuarioActivo;
        }


        public Usuario ObtenerUsuarioEditar(int idUsuario)
        {
            Usuario usuario = context.Usuario.Where(e => e.Id == idUsuario).FirstOrDefault();

            return usuario;
        }

        //Modifico los datos del usuario(Editar perfil):
        public void ModificarDatosUsuario(int id, string nombre, string apellido, string dni, string telefono, string geocomplete2)
        {
            Usuario usuario = context.Usuario.Where(e => e.Id == id).FirstOrDefault();
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Dni = dni;
            usuario.Ubicacion = geocomplete2;
            usuario.Telefono = telefono;

            context.SaveChanges();
        }

        //Modifico solo la contrasenia:
        public void GuardarContraseniaNueva(int id, string contrasenia)
        {
            Usuario user = context.Usuario.Where(e => e.Id == id).FirstOrDefault();
            user.Contrasenia = Encryptor.MD5Hash(contrasenia);
            context.SaveChanges();
        }


        public void DarDeBajaUsuario(int idUsuario)
        {
            Usuario miUser = context.Usuario.Where(e => e.Id == idUsuario).FirstOrDefault();
            miUser.IdEstado = 3; //Estado deshabilitado
            context.SaveChanges();
        }

        //Verificamos si el usuario que intenta registrarse fue dado de baja:
        public Usuario UsuarioDadoDeBaja(string p)
        {
            var usuario = (from user in context.Usuario
                           where user.Mail == p && user.IdEstado == 3  //Si el estado es 3 significa que esta deshabilitado
                           select user).First();
            return usuario;
        }


        public Usuario VerificarIdentidad(string mail)
        {
            var usuario = (from user in context.Usuario
                           where (user.Mail == mail && user.IdEstado == 1) 
                           select user).First();
            return usuario;
        }

        public void RestablecerContrasenia(string id, string contrasenia)
        {
            Usuario user = context.Usuario.Where(e => e.CodActivacion == id).FirstOrDefault();
            user.Contrasenia = Encryptor.MD5Hash(contrasenia);
            context.SaveChanges();
        }

        internal List<Usuario> traerPorZona(Usuario user, int limite)
        {
            return context.Usuario.Where(e => e.Ubicacion.Equals(user.Ubicacion)).Where(e => !e.Id.Equals(user.Id)).Take(limite).ToList();
        }

        internal Usuario traerPorId(int id)
        {
            return context.Usuario.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}