using System;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.ViewModels
{
    public class TestWithObjectSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public string EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
        public Subject SubjectObject { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
