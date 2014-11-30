﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class PublicacionRepositorio
    {

        ProvidereEntities context = new ProvidereEntities();

        internal List<Publicacion> traerPublicacionesPorZona(int limite)
        {
            var publicaciones = (from publicacion in context.Publicacion select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal ListaPublicacionesModel traerPublicacionesMasPopulares(int limite)
        {
            ListaPublicacionesModel publicaciones = new ListaPublicacionesModel();

                publicaciones.listadoDePublicaciones = (from publicacion in context.Publicacion 
                                 join puntaje in context.Puntaje on publicacion.Id equals puntaje.IdPublicacion
                                 orderby puntaje.Total descending
                                                        select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal List<Publicacion> traerPublicacionesMasNuevas(int limite)
        {
            var publicaciones = (from publicacion in context.Publicacion 
                                 orderby publicacion.FechaCreacion
                                 select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal List<Publicacion> buscarPorRubroSubRubroUbicacion(Rubro Rubro, SubRubro SubRubro, string Ubicacion)
        {
            if (String.IsNullOrEmpty(Rubro.ToString()) 
                && String.IsNullOrEmpty(SubRubro.ToString()) 
                && String.IsNullOrEmpty(Ubicacion))
            {

                var publicaciones = (from publicacion in context.Publicacion
                                     join usuario in context.Usuario on publicacion.IdUsuario equals usuario.Id
                                     where (publicacion.Rubro == Rubro) || (publicacion.SubRubro == SubRubro)
                                     || (usuario.Ubicacion == Ubicacion)
                                     select publicacion).ToList();

                return publicaciones;
            } 
            if (String.IsNullOrEmpty(Rubro.ToString())
                && String.IsNullOrEmpty(SubRubro.ToString())
                && String.IsNullOrEmpty(Ubicacion))
            {

                var publicaciones = (from publicacion in context.Publicacion
                                     join usuario in context.Usuario on publicacion.IdUsuario equals usuario.Id
                                     where (publicacion.Rubro == Rubro) || (publicacion.SubRubro == SubRubro)
                                     || (usuario.Ubicacion == Ubicacion)
                                     select publicacion).ToList();

                return publicaciones;
            }

            if (String.IsNullOrEmpty(Rubro.ToString())
                && String.IsNullOrEmpty(SubRubro.ToString())
                && String.IsNullOrEmpty(Ubicacion))
            {

                var publicaciones = (from publicacion in context.Publicacion
                                     join usuario in context.Usuario on publicacion.IdUsuario equals usuario.Id
                                     where (publicacion.Rubro == Rubro) || (publicacion.SubRubro == SubRubro)
                                     || (usuario.Ubicacion == Ubicacion)
                                     select publicacion).ToList();

                return publicaciones;
            }
            return null;
        }
    }
}