using System.Collections.Generic;
using WebApiAttempt1.DTO;

namespace WebApiAttempt1.Models
{
    public class SubjectsListDTO : SubjectDTO
    {
        public List<TestDTO> Tests { get; set; }
    }
}