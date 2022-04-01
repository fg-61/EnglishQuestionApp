using EnglishQuestionApp.Models.Entities.Test;
using System;

namespace EnglishQuestionApp.ViewModels
{
	public class QuizAnswerViewModel
	{
		public bool isCorrect { get; set;}
		public Guid QuestionId { get; set; }
		public Guid OptionId { get; set; }
	}
}
