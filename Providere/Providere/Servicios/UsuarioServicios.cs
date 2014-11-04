﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

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
        public void ActivarUsuarioInactivo(Usuario model)
        {
            var user = ur.TraerDatosPorMail(model.Mail); //Traigo todos los datos de ese usuario que tengo en la BD
            user.Nombre = model.Nombre;
            user.Apellido = model.Apellido;
            user.Contrasenia = Encryptor.MD5Hash(model.Contrasenia); 
            user.Telefono = model.Telefono;
            user.Ubicacion = model.Ubicacion;

            //Le envio el mail de confirmacion y actualizo los datos:
            mailing.EnviarMail(user);
            ur.ModificarUsuario(user);
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
    }
}