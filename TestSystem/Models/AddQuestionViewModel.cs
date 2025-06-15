using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestSystem.Models
{
    public class AddQuestionViewModel
    {
        public int TestId { get; set; }

        [Required]
        public string Text { get; set; }

        public List<AnswerViewModel> Answers { get; set; } = new();
    }
}

