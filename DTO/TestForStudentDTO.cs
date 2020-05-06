using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAttempt1.Models;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.DTO
{
    public class TestForStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public string EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreationDate { get; set; }
        public Subject SubjectObject { get; set; }
        public List<QuestionWithAnswersWithoutStatus> Questions { get; set; }
    }

    public class AnswerWithoutStatus
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Value { get; set; }
    }

    public class QuestionWithAnswersWithoutStatus
    {
        public Question Question { get; set; }
        public List<AnswerWithoutStatus> Answers { get; set; }
    }

}
