using LiftServiceWebApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishQuestionApp.Models.Entities.Test
{
    public class Question : BaseEntity<Guid>
    {
        public int QuestionNo { get; set; } // Soru numarasi
        public string QuestionContent { get; set; } // Soru icerigi
        public OptionCharacter CorrectAnswer { get; set; } // Dogru secenek

        // Hangi teste bagli oldugunu ifade edyor
        public Guid TestId { get; set; }
        [ForeignKey(nameof(TestId))]
        public virtual Test Test { get; set; }

        // Birden fazla secenek iceriyor
        public virtual List<Option> Options { get; set; }
    }
}
