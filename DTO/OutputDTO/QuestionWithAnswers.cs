using System;
using System.Collections.Generic;
using TestsBackend.DTO.ModelsDTO;
using TestsBackend.Models;

namespace TestsBackend.ViewModels
{
    public class QuestionWithAnswers : QuestionDTO
    {
        public List<Answer> Answers { get; set; }

        public QuestionWithAnswers()
        {
            Answers = new List<Answer>();
        }
    }
}
