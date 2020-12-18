using System;

namespace WebApiAttempt1.DTO
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public string EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
