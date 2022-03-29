using EnglishQuestionApp.ViewModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EnglishQuestionApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string urlHome = "https://www.wired.com";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlHome);

            List<MostRecentViewModel> mostRecents = new List<MostRecentViewModel>();

            for (int i=1; i<=5; i++)
            {
                List<string> paragraphs = new List<string>();

                HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='main-content']/div[1]/div[1]/section/div[3]/div/div/div/div/div[" + i + "]/div[2]/a/h2");
                if (titleNode != null)
                {
                    string title = titleNode.InnerText;
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
                        paragraphs.Add(decodedstring);
                    }
                    mostRecents.Add(new MostRecentViewModel
                    {
                        Title = title,
                        Paragraphs = paragraphs
                    });
                }
            }
            return View(mostRecents);
        }

        [HttpPost]
        public IActionResult Index(MostRecentViewModel model)
        {
            return RedirectToAction("GetTests","Home");
        }

        [HttpPost]
        public IActionResult GetText(List<MostRecentViewModel> texts, int textNumber)
        {
            List<string> paragraphs = texts[textNumber].Paragraphs;
            return View(paragraphs);
        }
    }
}
