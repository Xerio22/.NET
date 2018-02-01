using Microsoft.Ajax.Utilities;
using System;
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
        private static string pathToPhotosStorage = "~/Content/UploadedPhotos/";
        private readonly SchoolEntities _context = new SchoolEntities();


        public ActionResult PhotoLibrary()
        {
            var photos = _context.Photos.ToList();
            return View(new GalleryViewModel { Photos = photos });
        }


        [HttpPost]
        public ActionResult PhotoLibrary(Article article)
        {
            var photosInLibrary = _context.Photos.ToList();
            var model = new GalleryViewModel {
                Photos = photosInLibrary,
                Article = article
            };

            // when call comes from edit
            if (article.ArticleId != 0)
            {
                var photosInArticle = _context.Articles.Find(article.ArticleId).Photos;
                if (photosInArticle != null && photosInArticle.Count > 0)
                {
                    model.PhotosInArticle = photosInArticle;
                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult UploadPhotos(GalleryViewModel model)
        {
            var uploadedFiles = model.UploadedPhotos;
            if (uploadedFiles.Count > 0)
            {
                foreach (HttpPostedFileBase file in uploadedFiles)
                {
                    if (!IsFileValid(file, model))
                    {
                        model.Photos = _context.Photos.ToList();
                        return View("PhotoLibrary", model);
                    }

                    SaveFileInLibrary(file);
                }
            }

            return RedirectToAction("PhotoLibrary");
        }


        private bool IsFileValid(HttpPostedFileBase file, GalleryViewModel model)
        {
            if(IsFileEmpty(file))
            {
                model.FileErrorMessage = "Wstawianie pliku nie powiodło się";
                return false;
            }

            if (!HasValidExtension(file))
            {
                model.ExtensionErrorMessage = "Plik ma nieprawidłowe rozszerzenie. " +
                    "Dozwolone rozszerzenia wstawianych obrazów to: " + string.Join(", ", validExtensions.Select(v => v.ToString()));

                return false;
            }

            if (IsFileAlreadyExists(file))
            {
                model.FileErrorMessage = "Niektóre z plików o wybranych nazwach istnieją już w galerii";
                return false;
            }

            return true;
        }


        private bool IsFileEmpty(HttpPostedFileBase file)
        {
            return file == null || file.ContentLength <= 0;
        }


        private bool HasValidExtension(HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName);
            return validExtensions.Any(ext => ext == extension);
        }


        private bool IsFileAlreadyExists(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath(pathToPhotosStorage), fileName);

            return System.IO.File.Exists(path);
        }


        private void SaveFileInLibrary(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath(pathToPhotosStorage), fileName);

            // Save file in server's storage and put link to it in database
            file.SaveAs(path);
            _context.Photos.Add(
                new Photo() {
                    Link = Path.Combine(pathToPhotosStorage, fileName), // relative path
                    FileName = fileName
                }
            );
            _context.SaveChanges();
        }


        public ActionResult DeletePhoto(int photoId)
        {
            var photoToDelete = _context.Photos.First(p => p.PhotoId == photoId);

            // Delete photo from each article it belongs to
            photoToDelete.Articles.ForEach(art => art.Photos.Remove(art.Photos.First(ph => ph.PhotoId == photoToDelete.PhotoId)));
            _context.Photos.Remove(photoToDelete);
            _context.SaveChanges();

            // delete physically
            System.IO.File.Delete(Server.MapPath(photoToDelete.Link));

            return RedirectToAction("PhotoLibrary");
        }
    }
}