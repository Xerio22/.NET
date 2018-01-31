using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolaWebApp.DAL;

namespace SzkolaWebApp.Models
{
    public class InsertPhotosToArticleViewModel
    {
        public Article Article { get; set; }
        public ICollection<int> PhotosToInsertIDs { get; set; }
    }
}