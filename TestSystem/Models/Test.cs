namespace TestSystem.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
    }
}
