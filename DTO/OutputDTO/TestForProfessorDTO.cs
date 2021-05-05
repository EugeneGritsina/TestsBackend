using System.Collections.Generic;
using TestsBackend.DTO;
using TestsBackend.Models;
using TestsBackend.ViewModels;

namespace TestsBackend.JSONmodels
{
    //<description> Test with all connected questions and question answers have status here. </description>
    public class TestForProfessorDTO : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
        public List<QuestionWithAnswers> Questions { get; set; }
    }
}
