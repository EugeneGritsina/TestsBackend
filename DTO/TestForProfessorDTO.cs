using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebApiAttempt1.Models;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.JSONmodels
{
    //<description> Test with all connected questions and question answers have status here. </description>
    public class TestForProfessorDTO
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
        public List<QuestionWithAnswers> Questions { get; set; }
    }
}
