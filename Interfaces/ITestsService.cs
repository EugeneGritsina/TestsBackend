using WebApiAttempt1.JSONmodels;

namespace WebApiAttempt1.Services
{
    public interface ITestsService
    {
        public float CheckAnswers(TestForProfessorDTO testSentByUser);
    }
}