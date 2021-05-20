using TestsBackend.Models;

namespace TestsBackend.Services
{
    public interface ITestsService
    {
        public float CheckAnswers(TestForProfessorDTO testSentByUser);
    }
}