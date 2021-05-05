using TestsBackend.JSONmodels;

namespace TestsBackend.Services
{
    public interface ITestsService
    {
        public float CheckAnswers(TestForProfessorDTO testSentByUser);
    }
}