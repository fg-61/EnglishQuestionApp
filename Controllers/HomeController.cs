using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EnglishQuestionApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HtmlDocument mostRecentDocument;
            //string urlHome = "https://www.wired.com/";
            //HtmlWeb web = new HtmlWeb();
            //HtmlDocument doc = web.Load(urlHome);

            return View();
        }
    }
}
