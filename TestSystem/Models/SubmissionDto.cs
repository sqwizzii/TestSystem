namespace TestSystem.Models
{
    public class SubmissionDto
    {
        public int TestId { get; set; }
        public string UserId { get; set; }
        public List<SubmittedAnswer> Answers { get; set; }
    }
}
