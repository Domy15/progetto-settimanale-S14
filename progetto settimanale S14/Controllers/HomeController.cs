using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using progetto_settimanale_S14.Models;

namespace progetto_settimanale_S14.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static List<Article> _articles = new List<Article>();

        public IActionResult Index()
        {

            var articles = new ArticleViewModel() 
            { 
                Articles = _articles
            };

            return View(articles);
        }

        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost("articoli/aggiungi")]
        public IActionResult Create(AddArticleViewModel article)
        {
            if (!ModelState.IsValid) 
            {
                return View("Add", article);
            }

            var noVoidImages = article.Images.FindAll(x => !string.IsNullOrEmpty(x));

            var newArticle = new Article
            {
                Id = Guid.NewGuid(),
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                Cover = article.Cover,
                Images = noVoidImages,
            };

            _articles.Add(newArticle);

            return RedirectToAction("Index");
        }

        [HttpGet("article/detailes/{id:guid}")]
        public IActionResult Detailes(Guid id) 
        {
            var article = _articles.FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return RedirectToAction("Index");
            }

            var newArticle = new Article()
            {
                Id = article.Id, 
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                Cover = article.Cover,
                Images = article.Images,
            };

            return View(newArticle);
        }

        [HttpGet("article/edit/{id:guid}")]
        public IActionResult Edit(Guid id)
        {
            var article = _articles.FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return RedirectToAction("Index");
            }

            var editArticle = new EditArticle()
            {
                Id = article.Id,
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                Cover = article.Cover,
                Images = article.Images,
            };

            return View(editArticle);
        }

        [HttpPost("article/edit/{id:guid}")]
        public IActionResult SaveEdit(Guid id, EditArticle editArticle) 
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", editArticle);
            }

            var article = _articles.FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return RedirectToAction("Index");
            }

            var noVoidImages = editArticle.Images.FindAll(x => !string.IsNullOrEmpty(x));

            article.Name = editArticle.Name;
            article.Description = editArticle.Description;
            article.Price = editArticle.Price;
            article.Cover = editArticle.Cover;
            article.Images = noVoidImages;

            return RedirectToAction("Index");
        }

        [HttpGet("article/delete/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var article = _articles.FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return RedirectToAction("Index");
            }

            _articles.Remove(article);

            return RedirectToAction("Index");
        }
    }
}
