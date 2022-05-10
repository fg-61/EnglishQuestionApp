using EnglishQuestionApp.Data;
using EnglishQuestionApp.Models.Entities.Test;
using EnglishQuestionApp.ViewModels;
using EnglishQuestionApp.ViewModels.TestViewModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EnglishQuestionApp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using EnglishQuestionApp.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EnglishQuestionApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        static List<MostRecentViewModel> mostRecents = new List<MostRecentViewModel>();
        private readonly MyContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(MyContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
                    HtmlNodeCollection contentNodes = contentDoc.DocumentNode.SelectNodes("//div[@class='body__inner-container'][1]/p");
                    if (contentNodes != null)
                    {
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
            }

            ViewBag.MostRecents = mostRecents;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                Test test = new Test();
                var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
                if (user == null) return BadRequest();
                test.Title = model.Title;
                test.Paragraphs = model.Paragraphs;
                test.CreatedUser = user.Id;
                _dbContext.Add(test);
                foreach (QuestionViewModel questionViewModel in model.Questions)
                {
                    Question question = new Question();
                    question.TestId = test.Id;
                    question.QuestionContent = questionViewModel.QuestionContent;
                    question.CorrectAnswer = questionViewModel.CorrectAnswer;
                    question.CreatedUser = user.Id;
                    _dbContext.Add(question);
                    foreach (OptionViewModel optionViewModel in questionViewModel.Options)
                    {
                        Option option = new Option();
                        option.QuestionId = question.Id;
                        option.Character = optionViewModel.Character;
                        option.OptionContent = optionViewModel.OptionContent;
                        option.CreatedUser = user.Id;
                        _dbContext.Add(option);
                    }
                    test.Questions.Add(question);
                }
                _dbContext.SaveChanges();
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("GetTests", "Home");
        }

        [HttpPost]
        public JsonResult GetText(string textTitle)
        {
            string text = mostRecents.Find(x => x.Title == textTitle).Paragraphs;

            text += "<input type=\"hidden\" value=\"" + text + "\" id=\"Paragraphs\" name=\"Paragraphs\">"; // post islemi icin

            return Json(text);
        }

        [HttpGet]
        public async Task<IActionResult> GetTests()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            if (user == null) return BadRequest();

            List<Test> tests = _dbContext.Tests
                            .Where(x => x.CreatedUser == user.Id).ToList();

            return View(tests);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Test test = _dbContext.Tests.Find(id);
            if (test == null)
            {
                return BadRequest();
            }
            else
            {
                _dbContext.Remove(test);
                _dbContext.SaveChanges();
                return RedirectToAction("GetTests");
            }

        }

        [HttpGet]
        public IActionResult Quiz(Guid id)
        {
            Test test = _dbContext.Tests
                .Include(x => x.Questions.OrderBy(x => x.CreatedDate))
                .ThenInclude(x => x.Options.OrderBy(x => x.CreatedDate))
                .FirstOrDefault(x => x.Id == id);
            if (test == null)
            {
                return BadRequest();
            }
            else
            {
                return View(test);
            }
        }

        [HttpPost]
        public JsonResult CheckQuestionAnswers(List<QuizAnswerViewModel> request, string id)
        {
            var response = new List<QuizAnswerViewModel>();
            foreach (var questionAnswer in request)
            {
                QuizAnswerViewModel quiz = new QuizAnswerViewModel();
                var question = _dbContext.Questions.FirstOrDefault(x => x.Id == questionAnswer.QuestionId);
                var option = _dbContext.Options.FirstOrDefault(x => x.QuestionId == question.Id && x.Character == question.CorrectAnswer);
                if(questionAnswer.OptionId == option.Id)
                {
                    quiz.isCorrect = true;
                }
                else
                {
                    quiz.isCorrect = false;
                }
                quiz.OptionId = questionAnswer.OptionId;
                quiz.QuestionId = questionAnswer.QuestionId;
                response.Add(quiz);
            }

            return Json(response);
        }
    }
}
