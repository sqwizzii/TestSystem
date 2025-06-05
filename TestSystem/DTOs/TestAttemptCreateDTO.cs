using System.Collections.Generic;

namespace TestSystemAPI.DTOs
{
    public class TestAttemptCreateDTO
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public List<int> SelectedAnswerIds { get; set; } = new();
    }
}
