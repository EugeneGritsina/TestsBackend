using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsBackend.Models;

namespace TestsBackend.DTO.InputDTO
{
    public class InputQuestionWithAnswers : Question
    {
        public List<Answer> Answers { get; set; }

        public InputQuestionWithAnswers()
        {
            Answers = new List<Answer>();
        }
    }
}
