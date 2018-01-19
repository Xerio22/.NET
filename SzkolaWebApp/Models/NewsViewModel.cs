using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolaWebApp.Controllers;

namespace SzkolaWebApp.Models
{
    public class NewsViewModel
    {
        public bool IsUserAuthenticated {
            get
            {
                return HttpContext.Current.Session["UserCredentials"] != null;
            }
            set { }
        }
    }
}