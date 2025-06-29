namespace TestSystem.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string? ImageUrl { get; set; }
        public QuestionType Type { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public enum QuestionType
    {
        SingleChoice,
        MultipleChoice,
        Text
    }
}
