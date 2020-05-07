using System;
using System.Collections.Generic;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.ViewModels
{
    public class QuestionWithAnswers : Question
    {
        public List<Answer> Answers { get; set; }

        public QuestionWithAnswers()
        {
            Answers = new List<Answer>();
        }
    }
}
