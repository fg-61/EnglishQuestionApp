using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace EnglishQuestionApp.ViewModels.TestViewModels
{
	public class TestViewModel
	{
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Paragraphs { get; set; } // Birden fazla paragraf iceriyor
        public virtual List<QuestionViewModel> Questions { get; set; } // Birden fazla soru iceriyor
    }
}
