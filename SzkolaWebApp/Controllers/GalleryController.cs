using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SzkolaWebApp.DAL;
using SzkolaWebApp.Models;

namespace SzkolaWebApp.Controllers
{
    public class GalleryController : Controller
    {
        private static string[] validExtensions = { ".png", ".jpg", ".jpeg", ".gif" };
        private readonly SchoolEntities _context = new SchoolEntities();

        public ActionResult PhotoLibrary()
        {
            var photos = _context.Photos.ToList();
            return View(photos);
        }

        [HttpPost]
        public ActionResult UploadPhoto(PhotosViewModel model)
        {
            var uploadedFiles = Request.Files;
            if (uploadedFiles.Count > 0)
            {
                foreach (HttpPostedFile file in Request.Files)
                {
                    if (!IsFileValid(file, model))
                    {
                        return View(model);
                    }

                    SaveFileInLibrary(file);
                }
            }

            return View();
        }


        private bool IsFileValid(HttpPostedFile file, PhotosViewModel model)
        {
            if(IsFileEmpty(file))
            {
                model.FileErrorMessage = "Wstawianie pliku nie powiodło się";
                return false;
            }

            if (!HasValidExtension(file))
            {
                model.ExtensionErrorMessage = "Plik ma nieprawidłowe rozszerzenie. " +
                    "Dozwolone rozszerzenia wstawianych obrazów to: .jpg, .jpeg, .png, .gif";

                return false;
            }

            if (IsFileAlreadyExists(file))
            {
                model.FileErrorMessage = "Plik o takiej nazwie już istnieje";
                return false;
            }

            return true;
        }

        private bool IsFileEmpty(HttpPostedFile file)
        {
            return file == null || file.ContentLength <= 0;
        }

        private bool HasValidExtension(HttpPostedFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            return validExtensions.Any(ext => ext == extension);
        }

        private bool IsFileAlreadyExists(HttpPostedFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/UploadedPhotos/"), fileName);

            return System.IO.File.Exists(path);
        }


        private void SaveFileInLibrary(HttpPostedFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/UploadedPhotos/"), fileName);
            file.SaveAs(path);
        }


        public ActionResult DeletePhoto(int photoId)
        {
            var photoToDelete = _context.Photos.First(p => p.PhotoId == photoId);
            _context.Photos.Remove(photoToDelete);

            return RedirectToAction("PhotoLibrary");
        }
    }
}