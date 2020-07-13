using System.Linq;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.ViewModels;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.Repositories
{
    public interface ITestsRepository
    {
        public IQueryable<TestWithObjectSubject> GetTestsWithObjectSubject();
        public TestForProfessorDTO GetTestForProfessor(int id);
        public TestForStudentDTO GetTestToCompleteToStudent(int id);
        public Test SaveTest(TestForProfessorDTO test);
        public void DeleteTest(int id);
        public string CloseOrOpenTest(int id);
        public string UpdateTest(TestForProfessorDTO test);
    }
}