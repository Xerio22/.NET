using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SzkolaWebApp.Models
{
    public class LoginViewModel
    {
        public string ErrorMessage { get; set; }
        public UserCredentials Credentials { get; set; }
    }
}