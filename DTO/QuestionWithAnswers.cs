using System;
using System.Collections.Generic;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.ViewModels
{
    public class QuestionWithAnswers
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }

        public QuestionWithAnswers()
        {
            Question = new Question();
            Answers = new List<Answer>();
        }
    }
}
