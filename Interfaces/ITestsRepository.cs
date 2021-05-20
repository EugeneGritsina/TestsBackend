using System.Linq;
using TestsBackend.Models;

namespace TestsBackend.Repositories
{
    public interface ITestsRepository
    {
        public IQueryable<TestWithObjectSubject> GetTestsWithObjectSubject(int itemsAmount, int pageNumber);
        public IQueryable<TestTableModel> GetTestsForTable();
        //public IQueryable<TestWithObjectSubject> GetTestsWithObjectSubject();
        public TestForProfessorDTO GetTestForProfessor(int id);
        public TestForStudentDTO GetTestToCompleteToStudent(int id);
        public string PostCreateTest(InputTestDTO test);
        public void DeleteTest(int id);
        public string CloseOrOpenTest(int id);
        public string UpdateTest(InputTestDTO test);
    }
}