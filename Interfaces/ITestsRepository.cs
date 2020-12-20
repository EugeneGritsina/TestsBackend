using System.Linq;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.ViewModels;
using WebApiAttempt1.Models;
using WebApiAttempt1.DTO.InputDTO;

namespace WebApiAttempt1.Repositories
{
    public interface ITestsRepository
    {
        public IQueryable<TestWithObjectSubject> GetTestsWithObjectSubject();
        public TestForProfessorDTO GetTestForProfessor(int id);
        public TestForStudentDTO GetTestToCompleteToStudent(int id);
        public Test CreateTest(InputTestDTO test);
        public void DeleteTest(int id);
        public string CloseOrOpenTest(int id);
        public string UpdateTest(InputTestDTO test);
    }
}