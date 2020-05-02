using Microsoft.OData.Edm;
using System.Collections.Generic;

namespace WebApiAttempt1.Models
{
    public class SubjectsListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectTypeId { get; set; }
        public Date CreationDate { get; set; }
        public List<Test> Tests { get; set; }
    }
}