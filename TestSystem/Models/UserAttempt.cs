namespace TestSystem.Models
{
    public class UserAttempt
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TestId { get; set; }
        public int AttemptCount { get; set; }
    }
}
