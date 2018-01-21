using System.Collections.Generic;
using SzkolaWebApp.DAL;

namespace SzkolaWebApp.Models
{
    public class ArticlesViewModel : IAuthentication
    {
        public Article Article { get; set; }
        public IList<Article> Articles { get; set; }
        public bool IsUserAuthenticated { get; set; }
    }
}