using System.Collections.Generic;
using WebApiAttempt1.DTO;
using WebApiAttempt1.Models;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.JSONmodels
{
    //<description> Test with all connected questions and question answers have status here. </description>
    public class TestForProfessorDTO : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
        public List<QuestionWithAnswers> Questions { get; set; }
    }
}
