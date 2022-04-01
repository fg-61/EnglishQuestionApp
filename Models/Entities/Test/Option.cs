using LiftServiceWebApp.Models.Abstracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishQuestionApp.Models.Entities.Test
{
    public class Option : BaseEntity<Guid>
    {
        public OptionCharacter Character { get; set; } // Secenek Harfi
        public string OptionContent { get; set; } // Seceengin icerigi

        // Bagli oldugu soruyu ifade ediyor
        public Guid QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; }
    }

    public enum OptionCharacter
    {
        A,
        B,
        C,
        D
    }
}
