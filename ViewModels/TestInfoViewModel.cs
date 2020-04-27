using System;

namespace WebApiAttempt1.ViewModels
{
    public class TestInfoViewModel      //класс отличается тем, что вместо SubjectId у него SubjectName
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public TimeSpan? EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
    }
}
