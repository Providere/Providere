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
        public bool UsuarioExisteActivado(string email, string dni)
        {
            try
            {
                Usuario user = ur.UsuarioExisteActivado(email,dni);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Verificar si ya existe un usuario registrado pero inactivo con ese email:
        public bool UsuarioExisteInactivo(string email, string dni)
        {
            try
            {
                Usuario user = ur.UsuarioExisteInactivo(email,dni);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Activar el usuario que esta registrado pero inactivo:
        public void ActivarUsuarioInactivo(string nombre, string apellido, string dni,string mail, string telefono, string contrasenia, string geocomplete)
        {
            var user = ur.TraerDatosPorMailDni(mail,dni); //Traigo todos los datos de ese usuario
            user.Nombre = nombre;
            user.Apellido = apellido;
            user.Dni = dni;
            user.Mail = mail;
            user.Contrasenia = Encryptor.MD5Hash(contrasenia);
            user.Telefono = telefono;
            user.Ubicacion = geocomplete;
            user.CodActivacion = Encryptor.MD5Hash(mail);

            //Le envio el mail de confirmacion y actualizo los datos:

            ur.ModificarUsuario(user);
            mailing.EnviarMail(user);
        }

        public void AgregarUsuarioNuevo(string nombre, string apellido, string dni, string mail, string telefono, string contrasenia, string geocomplete)
        {
            Usuario miUsuario = new Usuario();
            miUsuario.Nombre = nombre;
            miUsuario.Apellido = apellido;
            miUsuario.Dni = dni;
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

        public object traerIdUsuario(Usuario model)
        {
            Usuario miUsuario = ur.TraerDatosPorMailDni(model.Mail,model.Dni);
            return miUsuario.Id;
        }

        //public List<Usuario> traerPorZona(Usuario user,int limite)
        //{
        //    return ur.traerPorZona(user,limite);
        //}

        public List<Usuario> traerPorZonaTodos(Usuario user)
        {
            return ur.traerPorZonaTodos(user);
        }

        public void CrearCookie(Usuario model)
        {
            var user = ur.TraerDatosPorMailDni(model.Mail,model.Dni);
            FormsAuthentication.SetAuthCookie(user.Nombre, false);
        }

        //Obtener usuario a traves de su id para editar datos:
        public Usuario ObtenerUsuarioEditar(int idUsuario)
        {
            return ur.ObtenerUsuarioEditar(idUsuario);
        }

        //Modifico datos personales:
        public void ModificarDatosUsuario(int id, string nombre, string apellido, string dni, string telefono, string geocomplete2)
        {
            ur.ModificarDatosUsuario(id, nombre, apellido, dni, telefono, geocomplete2);
        }

        //Modifico contraseña solo si el usuario desea cambiarla:
        public void GuardarContraseniaNueva(int id, string contrasenia)
        {
            ur.GuardarContraseniaNueva(id, contrasenia);
        }

        //Damos de baja al usuario:
        public void DarDeBajaUsuario(int idUsuario)
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

        //Verificar si existe el usuario que quiere recuperar contraseña:
        public bool VerificarIdentidad(string mail)
        {
            try
            {
                Usuario usuario = ur.VerificarIdentidad(mail);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Si  existe se le envia mail para recuperacion de contraseña:
        public void MailRecuperarContrasenia(string mail)
        {
            Usuario miUsuario = new Usuario();
            miUsuario.Mail = mail;
            miUsuario.CodActivacion = Encryptor.MD5Hash(mail);

            mailing.EnviarMailContrasenia(miUsuario);
        }
        
        //Se guarda la nueva contraseña para ese usuario:
        public void RestablecerContrasenia(string id, string contrasenia)
        {
            ur.RestablecerContrasenia(id, contrasenia);
        }

        internal Usuario traerUsuario(int id)
        {
            return ur.traerPorId(id);
        }

        public IEnumerable<Usuario> traerTodosConDenuncias()
        {
            DenunciaRepositorio dr = new DenunciaRepositorio();

            var listaUsuariosConDenuncias = ur.traerTodosConDenuncias(dr.traerDenunciasDelMes()); 
            return listaUsuariosConDenuncias;
        }

        public bool UsuarioBloqueado(Usuario model)
        {
            return ur.UsuarioBloqueado(model);
        }

        internal List<Usuario> traerPrestadoresPorZona(Usuario usuario, int limite)
        {
            return ur.traerPrestadoresPorZona(usuario, limite);
        }
    }
}