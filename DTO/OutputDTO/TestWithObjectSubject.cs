﻿using WebApiAttempt1.DTO;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.ViewModels
{
    public class TestWithObjectSubject : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
    }
}