using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SzkolaWebApp.DAL;
using SzkolaWebApp.Models;

namespace SzkolaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolEntities _context = new SchoolEntities();

        public ActionResult Articles()
        {
            ViewBag.Title = "Aktualności";
            var model = new ArticlesViewModel();
            
            var isUserAuthenticated = Session["UserCredentials"] != null;

            if (isUserAuthenticated)
            {
                model.Article = new Article();
            }

            model.IsUserAuthenticated = isUserAuthenticated;
            model.Articles = _context.Articles.OrderByDescending(art => art.PublicationDate).ToList();
            

            return View(model);
        }


        [HttpPost]
        public ActionResult AddArticle(ArticlesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Articles", model);
            }
            
            var username = ((UserCredentials)Session["UserCredentials"]).Username;
            model.Article.RegisteredUser = _context.RegisteredUsers.First(user => user.Nickname == username);
            model.Article.PublicationDate = DateTime.Now;

            _context.Articles.Add(model.Article);
            _context.SaveChanges();

            return RedirectToAction("Articles");
        }


        public ActionResult History()
        {
            ViewBag.Title = "Historia szkoły";
            return View();
        }
        

        public ActionResult Contact()
        {
            ViewBag.Title = "Kontakt";
            var viewModel = new ContentEditViewModel();
            
            viewModel.IsUserAuthenticated = Session["UserCredentials"] != null;
            viewModel.Content = _context.SiteContents.First(siteContent => siteContent.SiteContentId == 1).Content;
            
            return View(viewModel);
        }

        public ActionResult ContactEditMode()
        {
            ViewBag.Title = "Edycja zakładki kontakt";
            var viewModel = new ContentEditViewModel
            {
                Content = _context.SiteContents.First(siteContent => siteContent.SiteContentId == 1).Content
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ContactEditMode(ContentEditViewModel model)
        {
            if(model.Content.Length > 2000)
            {
                model.ErrorMessage = "Wprowadzona treść jest za długa!";

                return View(model);
            }
            
            var siteContent = _context.SiteContents.First(sC => sC.SiteContentId == 1);
            siteContent.Content = model.Content;
            _context.SaveChanges();

            return RedirectToAction("Contact");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}