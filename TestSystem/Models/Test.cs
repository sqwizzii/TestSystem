using System.Collections.Generic;

namespace TestSystemAPI.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
