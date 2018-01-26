using System.Collections.Generic;
using SzkolaWebApp.DAL;

namespace SzkolaWebApp.Models
{
    public class ArticlesViewModel : IAuthentication
    {
        public Article Article { get; set; }
        public IList<Article> Articles { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public string TitleErrorMessage { get; set; }
        public string ContentErrorMessage { get; set; }
        public HeaderModes HeaderMode { get; set; }
    }

    public enum HeaderModes
    {
        ADD, EDIT
    }
}