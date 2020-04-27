using System.Collections.Generic;

namespace WebApiAttempt1.Models
{
    public class TestsListViewModel
    {
        public Subject subject { get; set; }
        public List<Test> tests { get; set; }
    }
}