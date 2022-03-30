using EnglishQuestionApp.Models.Entities.Test;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishQuestionApp.ViewModels.TestViewModels
{
	public class QuestionViewModel
	{
        [Required(ErrorMessage = "Soru içeriği boş geçilemez")]
        public string QuestionContent { get; set; } // Soru icerigi
        [Required(ErrorMessage = "Doğru seçeneği seçiniz")]
        public OptionCharacter CorrectAnswer { get; set; } // Dogru secenek
        public virtual List<OptionViewModel> Options { get; set; }
    }
}
