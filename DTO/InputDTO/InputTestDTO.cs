﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.DTO.InputDTO
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