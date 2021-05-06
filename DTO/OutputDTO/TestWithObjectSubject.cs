using TestsBackend.DTO;

namespace TestsBackend.ViewModels
{
    public class TestWithObjectSubject : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
    }
}
