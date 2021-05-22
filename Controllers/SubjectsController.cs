using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TestsBackend.Models;

namespace TestsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        TestsContext _testsContext;
        public SubjectsController(TestsContext testsContext) => _testsContext = testsContext;

        [HttpGet("{itemsAmount}, {pageNumber}")]
        [Produces("application/json")]
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
                                select new TestDTO {
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

        [HttpGet]
        [Produces("application/json")]
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

        [HttpGet]
        [Route("list")]
        [Produces("application/json")]
        public object GetSubjects()
        {
            return (from s in _testsContext.Subjects
                    select new
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList();
        }
    }
}
