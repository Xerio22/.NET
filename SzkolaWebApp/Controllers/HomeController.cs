using System.Linq;
using System.Web.Mvc;
using SzkolaWebApp.DAL;
using SzkolaWebApp.Models;

namespace SzkolaWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Articles()
        {
            ViewBag.Title = "Aktualności";
            var model = new ArticlesViewModel();

            using (var _context = new SchoolEntities())
            {
                model.IsUserAuthenticated = Session["UserCredentials"] != null;
                model.Articles = _context.Articles.ToList();
            }

            return View(model);
        }


        public ActionResult History()
        {
            ViewBag.Title = "Historia szkoły";
            return View();
        }


        [HttpPost]
        public ActionResult AddArticle(ArticlesViewModel model)
        {
            return View("Articles");
        }


        public ActionResult Contact()
        {
            ViewBag.Title = "Kontakt";
            var viewModel = new ContentEditViewModel();

            using (var _context = new SchoolEntities())
            {
                viewModel.IsUserAuthenticated = Session["UserCredentials"] != null;
                viewModel.Content = _context.SiteContents.First(siteContent => siteContent.SiteContentId == 1).Content;
            }

            return View(viewModel);
        }

        public ActionResult ContactEditMode()
        {
            ViewBag.Title = "Edycja zakładki kontakt";
            var viewModel = new ContentEditViewModel();

            using (var _context = new SchoolEntities())
            {
                viewModel.Content = _context.SiteContents.First(siteContent => siteContent.SiteContentId == 1).Content;
            }

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

            using (var _context = new SchoolEntities())
            {
                var siteContent = _context.SiteContents.First(sC => sC.SiteContentId == 1);
                siteContent.Content = model.Content;
                _context.SaveChanges();

                return RedirectToAction("Contact");
            }
        }
    }
}