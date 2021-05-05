using TestsBackend.DTO;
using TestsBackend.Models;

namespace TestsBackend.ViewModels
{
    public class TestWithObjectSubject : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
    }
}
