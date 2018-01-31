using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolaWebApp.DAL;

namespace SzkolaWebApp.Models
{
    public class GalleryViewModel
    {
        public string ExtensionErrorMessage { get; set; }
        public string FileErrorMessage { get; set; }
        public IList<Photo> Photos { get; set; }
        public IList<HttpPostedFileBase> UploadedPhotos { get; set; }
        public Article Article { get; set; }
    }
}