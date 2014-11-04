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
            usuario.Contrasenia = Encryptor.MD5Hash(user.Contrasenia);
            usuario.Telefono = user.Telefono;
            usuario.Ubicacion = user.Ubicacion;
            //El estado y la fecha de activacion se modifica despues que mando mail y activo la cuenta
            entities.SaveChanges();

        }

        internal void CrearUsuario(Usuario model)
        {
            model.Contrasenia = Encryptor.MD5Hash(model.Contrasenia); 
            model.FechaActivacion = DateTime.Now;
            model.FechaCreacion = DateTime.Now;
            model.FechaCambioEstado = DateTime.Now;
            model.IdEstado = Convert.ToInt16(4); //Estado inactivo hasta que confirma su registracion
            model.CodActivacion = Encryptor.MD5Hash(model.Mail); 
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
    }
}