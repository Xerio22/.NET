using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzkolaWebApp.Models
{
    public interface IAuthentication
    {
        bool IsUserAuthenticated { get; set; }
    }
}
