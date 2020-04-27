using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAttempt1.ViewModels
{
    public class WholeTestCreateUpdateModel
    {
        public CreateUpdateTest Test { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
