using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SzkolaWebApp.DAL;
using SzkolaWebApp.Models;

namespace SzkolaWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
        }
        
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            Session["UserCredentials"] = new UserCredentials()
            {
                Username = "SomeUserName"
            };

            return View();
        }
        
        [HttpPost]
        public ActionResult Login(UserCredentials credentials)
        {
            using (var _context = new SchoolEntities())
            {
                var passwordHash = Encoding.ASCII.GetBytes(credentials.Password); 

                var userId = _context.RegisteredUsers.Where(user => user.Nickname == credentials.Username && user.PasswordHash == passwordHash);

                Session["UserId"] = userId;
            }

                

            return View();
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
    }
}