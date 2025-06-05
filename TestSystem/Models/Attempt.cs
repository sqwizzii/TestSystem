namespace TestSystem.Models;

public class Attempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int TestId { get; set; }
    public Test Test { get; set; }

    public DateTime StartTime { get; set; }
    public int Score { get; set; }
}
