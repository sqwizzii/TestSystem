namespace TestSystem.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Question> Questions { get; set; }
    }

}
