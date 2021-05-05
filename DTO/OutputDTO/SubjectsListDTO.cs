using System.Collections.Generic;
using TestsBackend.DTO;

namespace TestsBackend.Models
{
    public class SubjectsListDTO : SubjectDTO
    {
        public List<TestDTO> Tests { get; set; }
    }
}