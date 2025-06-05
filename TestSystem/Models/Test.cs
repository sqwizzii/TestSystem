namespace TestSystem.Models;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<Question> Questions { get; set; }
    public ICollection<Attempt> Attempts { get; set; }
}
