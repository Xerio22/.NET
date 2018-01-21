using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SzkolaWebApp.DAL;
using SzkolaWebApp.Models;

namespace SzkolaWebApp.Controllers
{
    public class AccountController : Controller
    {   
        public ActionResult Register()
        {
            ViewBag.Title = "Rejestracja";

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginViewModel model)
        {
            ViewBag.Title = "Rejestracja";

            using (var _context = new SchoolEntities())
            {
                var isUserWithThatNicknameExists = _context.RegisteredUsers.Any(user => user.Nickname == model.Credentials.Username);

                if (isUserWithThatNicknameExists)
                {
                    model.ErrorMessage = "Użytkownik o takiej nazwie już istnieje";
                    return View(model);
                }
                else
                {
                    var passwordHash = PasswordSecurity.PasswordStorage.CreateHash(model.Credentials.Password);

                    _context.RegisteredUsers.Add(new RegisteredUser() { Nickname = model.Credentials.Username, PasswordHash = passwordHash, UserTypeId = 2 } );
                    _context.SaveChanges();

                    Session["UserCredentials"] = new UserCredentials()
                    {
                        Username = model.Credentials.Username
                    };

                    return RedirectToAction("Articles", "Home");
                }
            }
        }


        public ActionResult Login()
        {
            ViewBag.Title = "Logowanie";

            if (Session["UserCredentials"] != null)
                return RedirectToAction("Articles", "Home");
            else
                return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            ViewBag.Title = "Logowanie";

            if (Session["UserCredentials"] == null)
            {
                using (var _context = new SchoolEntities())
                {
                    var user = _context.RegisteredUsers.FirstOrDefault(u => u.Nickname == model.Credentials.Username);
                    
                    if (user != null)
                    {
                        var correctPasswordHash = _context.RegisteredUsers.FirstOrDefault(u => u.Nickname == model.Credentials.Username).PasswordHash;
                        var areCredentialsCorrect = PasswordSecurity.PasswordStorage.VerifyPassword(model.Credentials.Password, correctPasswordHash);

                        if (areCredentialsCorrect)
                        {
                            Session["UserCredentials"] = new UserCredentials()
                            {
                                Username = model.Credentials.Username
                            };
                        }
                        else
                        {
                            model.ErrorMessage = "Wprowadzono nieprawidłowe dane logowania";
                            return View(model);
                        }
                    }
                    else
                    {
                        model.ErrorMessage = "Wprowadzono nieprawidłowe dane logowania";
                        return View(model);
                    }
                }
            }

            return RedirectToAction("Articles", "Home");
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Articles", "Home");
        }
    }
}