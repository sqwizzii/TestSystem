namespace TestSystem.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
