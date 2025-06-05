using static System.Net.Mime.MediaTypeNames;

namespace TestSystem.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsMultipleChoice { get; set; }
    public int Weight { get; set; }

    public int TestId { get; set; }
    public Test Test { get; set; }

    public ICollection<Answer> Answers { get; set; }
    public Image? Image { get; set; }
}
