using LiftServiceWebApp.Models.Abstracts;
using System;
using System.Collections.Generic;

namespace EnglishQuestionApp.Models.Entities.Test
{
    public class Test : BaseEntity<Guid>
    {
        public int TestNo { get; set; }
        public string Title { get; set; }
        public string Paragraphs { get; set; } // Birden fazla paragraf iceriyor
        public virtual List<Question> Options { get; set; } // Birden fazla soru iceriyor
    }
}
