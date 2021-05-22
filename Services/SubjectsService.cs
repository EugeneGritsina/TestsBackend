using System.Collections.Generic;
using System.Linq;
using TestsBackend.Interfaces;
using TestsBackend.Models;

namespace TestsBackend.Services
{
    public class SubjectsService : ISubjectsService
    {
        TestsContext _testsContext;
        public SubjectsService(TestsContext testsContext) => _testsContext = testsContext;

        public List<SubjectsListDTO> GetSubjectsWithListOfTests(int itemsAmount, int pageNumber)
        {
            return (from s in _testsContext.Subjects
                    select new SubjectsListDTO
                    {
                        Id = s.Id,
                        SubjectType = (from subjectType in _testsContext.SubjectTypes
                                       where subjectType.Id == s.SubjectTypeId
                                       select subjectType).First(),
                        Name = s.Name,
                        Tests = (from t in _testsContext.Tests
                                 where t.SubjectId == s.Id
                                 select new TestDTO
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     DueDateTime = t.DueDateTime,
                                     EstimatedTime = t.EstimatedTime,
                                     QuestionsAmount = t.QuestionsAmount,
                                     MaxMark = t.MaxMark,
                                     IsOpen = t.IsOpen,
                                     CreationDate = t.CreationDate
                                 }).ToList()
                    }).Skip(itemsAmount * (pageNumber - 1)).Take(itemsAmount).ToList();
        }

        public List<SubjectsListDTO> GetSubjectsWithListOfTests()
        {
            return (from s in _testsContext.Subjects
                    select new SubjectsListDTO
                    {
                        Id = s.Id,
                        SubjectType = (from subjectType in _testsContext.SubjectTypes
                                       where subjectType.Id == s.SubjectTypeId
                                       select subjectType).First(),
                        Name = s.Name,
                        Tests = (from t in _testsContext.Tests
                                 where t.SubjectId == s.Id
                                 select new TestDTO
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     DueDateTime = t.DueDateTime,
                                     EstimatedTime = t.EstimatedTime,
                                     QuestionsAmount = t.QuestionsAmount,
                                     MaxMark = t.MaxMark,
                                     IsOpen = t.IsOpen,
                                     CreationDate = t.CreationDate
                                 }).ToList()
                    }).ToList();
        }

        public List<SubjectDTO> GetSubjects()
        {
            return (from s in _testsContext.Subjects
                    select new SubjectDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        SubjectType = (from type in _testsContext.SubjectTypes select type).First()
                    }).ToList();
        }
    }
}