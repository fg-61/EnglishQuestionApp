using LiftServiceWebApp.Models.Abstracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishQuestionApp.Models.Entities.Test
{
    public class Paragraph : BaseEntity<Guid>
    {
        public string ParagraphText { get; set; }
        public Guid TestId { get; set; }
        [ForeignKey(nameof(TestId))]
        public virtual Test Test { get; set; }
    }
}
