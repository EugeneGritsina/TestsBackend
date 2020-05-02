using System;
using System.Collections.Generic;
using WebApiAttempt1.Models;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.JSONmodels
{
    public class TestPassingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreationDate { get; set; }
        public Subject SubjectObject { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
