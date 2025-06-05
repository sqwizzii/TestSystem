namespace TestSystem.Models;

public class Image
{
    public int Id { get; set; }
    public string FileName { get; set; } // Назва файлу
    public byte[] Data { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
