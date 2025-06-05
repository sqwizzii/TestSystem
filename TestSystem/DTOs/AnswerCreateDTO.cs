namespace TestSystemAPI.DTOs
{
    public class AnswerCreateDTO
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
