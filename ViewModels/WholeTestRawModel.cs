using System.Collections.Generic;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.ViewModels
{
    public class WholeTestRawModel
    {
        public Test Test { get; set; }
        public List<QuestionViewModel> Questions { get; set; }

        public WholeTestRawModel()
        {
            Test = new Test();
            Questions = new List<QuestionViewModel>();
        }
    }
}
