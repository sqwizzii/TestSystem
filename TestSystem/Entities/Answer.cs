namespace TestSystem.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public List<Question> Questions { get; set; } = null!;
        public Question Question { get; set; }
    }
}
