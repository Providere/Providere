using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using System.Text.RegularExpressions;

namespace Providere.Controllers
{
    public class ImagenController : Controller
    {

        [HttpPost]
        public virtual ActionResult RecortarImagen(string imagePath, int? cropPointX, int? cropPointY, int? imageCropWidth, int? imageCropHeight)
        {
            if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            var recortadoImagePath = Regex.Replace(imagePath, @"[\?]\d*$", "");
            byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath(recortadoImagePath));
            byte[] croppedImage = ImageHelper.CropImage(imageBytes, cropPointX.Value, cropPointY.Value, imageCropWidth.Value, imageCropHeight.Value);

            string tempFolderName = Server.MapPath("~/Imagenes/FotoPerfil/" + ConfigurationManager.AppSettings["Image.TempFolderName"]);
            string fileName = Path.GetFileName(imagePath);

            fileName = Regex.Replace(fileName, @"(Temp-)", "");
            try
            {
                FileHelper.SaveFile(croppedImage, Path.Combine(tempFolderName, fileName));
            }
            catch (Exception ex)
            {
                //Log an error     
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            string fullPath = Server.MapPath("~/Imagenes/FotoPerfil/Temp-" + fileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }


            string photoPath = string.Concat("/Imagenes/FotoPerfil", ConfigurationManager.AppSettings["Image.TempFolderName"], "/", fileName);
            //return Json(new { photoPath = photoPath }, JsonRequestBehavior.AllowGet);
            return Json(new { result = "Redirect", url = Url.Action("EditarPerfil", "Usuario", new { ActivePanel = 2 }) });

        }


        [HttpPost]
        public ActionResult CambiarFotoPerfil()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var file = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];


                string extension = Path.GetExtension(file.FileName);
                int id = Convert.ToInt16(this.Session["IdUsuario"]);
                if (file != null && file.ContentLength > 0 && extension == ".jpg")
                {
                    try
                    {
                        string uniqueFileName = Path.ChangeExtension("Temp-imagen", Convert.ToString(id));
                        string path = Path.Combine(Server.MapPath("~/Imagenes/FotoPerfil"),
                                           Path.GetFileName(uniqueFileName + extension));
                        file.SaveAs(path);

                        TempData["Exito"] = "Tu foto de perfil se ha cargado con exito";
                        return Json(new
                        {
                            path = Url.Content("/Imagenes/FotoPerfil/" + Path.GetFileName(uniqueFileName + extension))
                        });
                    }
                    catch (Exception ex)
                    {
                        ClientException.LogException(ex, "Error al cargar la imágen.");
                        return RedirectToAction("Error", "Shared");
                    }
                }
                else
                {
                    TempData["Error"] = "No se pudo cargar la imágen, intentalo nuevamente.";
                    return RedirectToAction("EditarPerfil", new { id = id });
                }

            }
            else
            {
                TempData["Error"] = "No se pudo cargar la imágen, intentalo nuevamente.";
                return RedirectToAction("EditarPerfil");
            };
        }
    }
}
