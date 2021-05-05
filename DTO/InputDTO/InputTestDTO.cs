using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsBackend.Models;

namespace TestsBackend.DTO.InputDTO
{
    public class InputTestDTO : Test
    {
        public List<InputQuestionWithAnswers> Questions { get; set; }

        public InputTestDTO()
        {
            Questions = new List<InputQuestionWithAnswers>();
        }
    }
}
