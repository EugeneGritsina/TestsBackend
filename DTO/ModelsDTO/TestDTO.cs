using System;

namespace TestsBackend.DTO
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
