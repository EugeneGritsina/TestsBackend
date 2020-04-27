using System;
using System.Collections.Generic;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.ViewModels
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
