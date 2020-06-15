using System.Linq;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.Repositories
{
    public interface ITestsRepository
    {
        public IQueryable<TestWithObjectSubject> GetTestsWithObjectSubject();
        public TestForProfessorDTO GetTestForProfessor(int id);
        public TestForStudentDTO SendTestToCompleteToStudent(int id);


    }
}