using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAttempt1.ViewModels
{
    public class WholeTestViewModel
    {
        public TestInfoViewModel Test { get; set; }
        public List<QuestionViewModel> Questions { get; set; }

        public WholeTestViewModel()
        {
            Test = new TestInfoViewModel();
            Questions = new List<QuestionViewModel>();
        }
    }
}
