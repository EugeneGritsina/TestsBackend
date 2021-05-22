using System.Collections.Generic;
using TestsBackend.Models;

namespace TestsBackend.Interfaces
{
    public interface ISubjectsService
    {
        public List<SubjectsListDTO> GetSubjectsWithListOfTests(int itemsAmount, int pageNumber);
        public List<SubjectsListDTO> GetSubjectsWithListOfTests();
        public List<SubjectDTO> GetSubjects();
    }
}
