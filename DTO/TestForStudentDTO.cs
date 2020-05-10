using System;
using System.Collections.Generic;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.DTO
{
    public class TestForStudentDTO : TestDTO
    {
        public Subject SubjectObject { get; set; }
        public List<QuestionWithAnswersWithoutStatus> Questions { get; set; }
    }

    public class AnswerWithoutStatus
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Value { get; set; }
    }

    public class QuestionWithAnswersWithoutStatus : Question
    {
        public List<AnswerWithoutStatus> Answers { get; set; }

        public QuestionWithAnswersWithoutStatus()
        {
            Answers = new List<AnswerWithoutStatus>();
        }
    }

}
