using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishQuestionApp.ViewModels.TestViewModels
{
	public class TestViewModel
	{
        public string Title { get; set; }
        public string Paragraphs { get; set; } // Birden fazla paragraf iceriyor
        public virtual List<QuestionViewModel> Questions { get; set; } // Birden fazla soru iceriyor
    }
}
