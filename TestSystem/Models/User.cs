namespace TestSystem.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // "student" або "teacher"

    public ICollection<Attempt> Attempts { get; set; }
}
