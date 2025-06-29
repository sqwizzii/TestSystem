namespace TestSystem.Models
{
    public class SubmittedAnswer
    {
        public int QuestionId { get; set; }
        public List<int>? SelectedAnswers { get; set; }
        public string? TextAnswer { get; set; }
    }
}
