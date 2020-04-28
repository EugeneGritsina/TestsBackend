using System.Collections.Generic;

namespace WebApiAttempt1.Models
{
    public class TestsListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectTypeId { get; set; }
        public List<Test> Tests { get; set; }
    }
}