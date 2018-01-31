﻿using System;
using System.Collections.Generic;
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
            var model = new ArticlesViewModel
            {
                IsUserAuthenticated = Session["UserCredentials"] != null,
                Articles = GetArticlesListFromDatabase(),
                HeaderMode = HeaderModes.ADD
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult InsertPhotosToArticle(InsertPhotosToArticleViewModel givenModel)
        {
            var photosToInsert = _context.Photos.Where(photo => givenModel.PhotosToInsertIDs.Contains(photo.PhotoId)).ToList();
            
            var model = new ArticlesViewModel
            {
                IsUserAuthenticated = Session["UserCredentials"] != null,
                Articles = GetArticlesListFromDatabase(),
                PhotosToInsert = photosToInsert,
                Article = givenModel.Article
            };
            return View("Articles", model);
        }


        [HttpPost]
        public ActionResult AddArticle(ArticlesViewModel model)
        {
            bool isModelValid = CheckArticleModelValidity(model);

            if (!isModelValid)
            {
                model.IsUserAuthenticated = Session["UserCredentials"] != null;
                model.Articles = GetArticlesListFromDatabase();
                return View("Articles", model);
            }
            
            var username = ((UserCredentials)Session["UserCredentials"]).Username;
            model.Article.RegisteredUser = _context.RegisteredUsers.First(user => user.Nickname == username);
            model.Article.PublicationDate = DateTime.Now;

            if (model.PhotosToInsertIDs != null)
            {
                var photosToInsert = _context.Photos.Where(photo => model.PhotosToInsertIDs.Contains(photo.PhotoId)).ToList();

                photosToInsert.ToList().ForEach(p => model.Article.Photos.Add(p));
                _context.SaveChanges();
            }

            _context.Articles.Add(model.Article);
            _context.SaveChanges();

            return RedirectToAction("Articles");
        }


        public ActionResult EditArticle(int articleId)
        {
            var article = _context.Articles.First(art => art.ArticleId == articleId);

            ArticlesViewModel model = new ArticlesViewModel()
            {
                Articles = GetArticlesListFromDatabase(),
                Article = article,
                IsUserAuthenticated = Session["UserCredentials"] != null,
                HeaderMode = HeaderModes.EDIT
            };

            return View("Articles", model);
        }


        [HttpPost]
        public ActionResult EditArticle(ArticlesViewModel model)
        {
            bool isModelValid = CheckArticleModelValidity(model);

            if (!isModelValid)
            {
                model.IsUserAuthenticated = Session["UserCredentials"] != null;
                model.Articles = GetArticlesListFromDatabase();
                return View("Articles", model);
            }

            var articleToUpdate = _context.Articles.First(art => art.ArticleId == model.Article.ArticleId);
            articleToUpdate.Title = model.Article.Title;
            articleToUpdate.Content = model.Article.Content;
            _context.SaveChanges();

            return RedirectToAction("Articles");
        }

        
        public ActionResult DeleteArticle(int articleId)
        {
            var articleToRemove = _context.Articles.First(art => art.ArticleId == articleId);
            _context.Articles.Remove(articleToRemove);
            _context.SaveChanges();

            return RedirectToAction("Articles");
        }


        // zrobione w ten sposób, aby wyświetlać wszystkie błędy bezpośrednio
        private bool CheckArticleModelValidity(ArticlesViewModel model)
        {
            bool validationResult = true;

            if (string.IsNullOrEmpty(model.Article.Title))
            {
                model.TitleErrorMessage = "~ Tytuł nie może być pusty";
                validationResult = false;
            }
            else if (model.Article.Title.Length > 60)
            {
                model.TitleErrorMessage = "~ Tytuł jest za długi. Maksymalna liczba znaków to 60";
                validationResult = false;
            }

            if (string.IsNullOrEmpty(model.Article.Content))
            {
                model.ContentErrorMessage = "~ Treść nie może być pusta";
                validationResult = false;
            }
            else if (model.Article.Content.Length > 4000)
            {
                model.ContentErrorMessage = "~ Treść jest za długa. Maksymalna liczba znaków to 4000";
                validationResult = false;
            }

            return validationResult;
        }


        public ActionResult History()
        {
            ViewBag.Title = "Historia szkoły";
            return View();
        }
        

        public ActionResult Contact()
        {
            ViewBag.Title = "Kontakt";
            var viewModel = new ContentEditViewModel
            {
                IsUserAuthenticated = Session["UserCredentials"] != null,
                Content = _context.SiteContents.First(siteContent => siteContent.SiteContentId == 1).Content
            };

            return View(viewModel);
        }


        public ActionResult ContactEditMode()
        {
            if(Session["UserCredentials"] == null)
            {
                return RedirectToAction("Contact");
            }

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
            if (model.Content == null)
            {
                model.Content = _context.SiteContents.First(sc => sc.SiteContentId == 1).Content;
                model.ErrorMessage = "Treść nie może być pusta";

                return View(model);
            }
            if (model.Content.Length > 2000)
            {
                model.ErrorMessage = "Wprowadzona treść jest za długa!";

                return View(model);
            }
            
            var siteContent = _context.SiteContents.First(sC => sC.SiteContentId == 1);
            siteContent.Content = model.Content;
            _context.SaveChanges();

            return RedirectToAction("Contact");
        }


        private IList<Article> GetArticlesListFromDatabase()
        {
            return _context.Articles.OrderByDescending(art => art.PublicationDate).ToList();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


        
        public ActionResult Page1()
        {
            var page = _context.Pages.First(p => p.PageId == 1);

            return View(page);
        }

        public ActionResult Page2()
        {
            var page = _context.Pages.First(p => p.PageId == 2);

            return View(page);
        }

        public ActionResult ShowParagraphs()
        {
            var pars = _context.Paragraphs.ToList();

            return View(pars);
        }


        public ActionResult AddParagraph()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddParagraph(Paragraph paragraph)
        {
            _context.Paragraphs.Add(paragraph);
            _context.SaveChanges();

            return RedirectToAction("ShowParagraphs");
        }
        
        public ActionResult RemoveParagraphFromPage(int idPage, int idParagraph)
        {
            var page = _context.Pages.First(pag => pag.PageId == idPage);
            var paragraph = _context.Paragraphs.First(par => par.ParagraphId == idParagraph);
            page.Paragraphs.Remove(paragraph);
            _context.SaveChanges();

            return RedirectToAction("Page1");
        }

        public ActionResult AddParagraphToPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddParagraphToPage(AddParagraphToPageViewModel model)
        {
            var page = _context.Pages.First(p => p.PageId == model.Page.PageId);
            var parag = _context.Paragraphs.First(p => p.ParagraphId == model.Paragraph.ParagraphId);

            page.Paragraphs.Add(parag);
            _context.SaveChanges();

            return View();
        }
    }
}