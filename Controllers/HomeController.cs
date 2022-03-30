using EnglishQuestionApp.Data;
using EnglishQuestionApp.Models.Entities.Test;
using EnglishQuestionApp.ViewModels;
using EnglishQuestionApp.ViewModels.TestViewModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace EnglishQuestionApp.Controllers
{
    public class HomeController : Controller
    {
        static List<MostRecentViewModel> mostRecents = new List<MostRecentViewModel>();
        private readonly MyContext _dbContext;
        public HomeController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            string urlHome = "https://www.wired.com";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlHome);

            mostRecents.Clear();

            for (int i = 1; i <= 5; i++)
            {
                string paragraphs = "";

                HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='main-content']/div[1]/div[1]/section/div[3]/div/div/div/div/div[" + i + "]/div[2]/a/h2");
                if (titleNode != null)
                {
                    string title = WebUtility.HtmlDecode(titleNode.InnerText); ;
                    // Get Content
                    HtmlNode contentLinkNode = doc.DocumentNode.SelectSingleNode("//*[@id='main-content']/div[1]/div[1]/section/div[3]/div/div/div/div/div[" + i + "]/div[2]/a[@href]");
                    string contentHrefValue = contentLinkNode.GetAttributeValue("href", string.Empty);
                    string contentUrl = urlHome + contentHrefValue;
                    HtmlDocument contentDoc = web.Load(contentUrl);
                    HtmlNodeCollection contentNodes = contentDoc.DocumentNode.SelectNodes("//*[@id='main-content']/article/div[2]/div[1]/div[1]/div[1]/div[1]/div/div/p");
                    foreach (HtmlNode contentNode in contentNodes)
                    {
                        string content = contentNode.InnerText;
                        string decodedstring = WebUtility.HtmlDecode(content); // Fix the special characters
                        string text = "<p>" + decodedstring + "</p>";
                        paragraphs += string.Concat(text);
                    }
                    mostRecents.Add(new MostRecentViewModel
                    {
                        Title = title,
                        Paragraphs = paragraphs
                    });
                }
            }

            ViewBag.MostRecents = mostRecents;
            return View();
        }

        [HttpPost]
        public IActionResult Index(TestViewModel model)
        {
            
            return RedirectToAction("GetTests", "Home");
        }

        [HttpPost]
        public JsonResult GetText(string textTitle)
        {
            string text = mostRecents.Find(x => x.Title == textTitle).Paragraphs;
            int index = mostRecents.FindIndex(x => x.Title == textTitle);

            text += "<input type=\"hidden\" value=\"" + text + "\" id=\"Paragraphs\" name=\"Paragraphs\">"; // post islemi icin

            return Json(text);
        }
    }
}
