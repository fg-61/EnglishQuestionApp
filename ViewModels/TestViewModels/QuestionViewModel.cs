using EnglishQuestionApp.Models.Entities.Test;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishQuestionApp.ViewModels.TestViewModels
{
	public class QuestionViewModel
	{
        public string QuestionContent { get; set; } // Soru icerigi
        public OptionCharacter CorrectAnswer { get; set; } // Dogru secenek
        public virtual List<OptionViewModel> Options { get; set; }
    }
}
