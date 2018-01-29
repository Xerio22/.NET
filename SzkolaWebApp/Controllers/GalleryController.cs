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
            return View(photos);
        }


        [HttpPost]
        public ActionResult UploadPhoto(GalleryViewModel model)
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

            return RedirectToAction("PhotoLibrary");
        }


        private bool IsFileValid(HttpPostedFile file, GalleryViewModel model)
        {
            if(IsFileEmpty(file))
            {
                model.FileErrorMessage = "Wstawianie pliku nie powiodło się";
                return false;
            }

            if (!HasValidExtension(file))
            {
                model.ExtensionErrorMessage = "Plik ma nieprawidłowe rozszerzenie. " +
                    "Dozwolone rozszerzenia wstawianych obrazów to: " + validExtensions.ToString();

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
            var path = Path.Combine(Server.MapPath(pathToPhotosStorage), fileName);

            return System.IO.File.Exists(path);
        }


        private void SaveFileInLibrary(HttpPostedFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath(pathToPhotosStorage), fileName);

            // Save file in server's storage and put link to it in database
            file.SaveAs(path);
            _context.Photos.Add(new Photo() { Link = path });
            _context.SaveChanges();
        }


        public ActionResult DeletePhoto(int photoId)
        {
            var photoToDelete = _context.Photos.First(p => p.PhotoId == photoId);
            _context.Photos.Remove(photoToDelete);
            _context.SaveChanges();

            return RedirectToAction("PhotoLibrary");
        }
    }
}