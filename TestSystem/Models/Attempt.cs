using System;

namespace TestSystemAPI.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }

        public User? User { get; set; }
        public Test? Test { get; set; }
    }
}
