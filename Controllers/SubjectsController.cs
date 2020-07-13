using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiAttempt1.DTO;
using WebApiAttempt1.Models;

namespace WebApiAttempt1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        TestsContext TestsContext;

        public SubjectsController(TestsContext testsContext) => TestsContext = testsContext;

       [HttpGet]
       [Produces("application/json")]
        public List<SubjectsListDTO> GetSubjects()
        {
            return (from s in TestsContext.Subjects
                    select new SubjectsListDTO
                    {
                        Id = s.Id,
                        SubjectTypeId = s.SubjectTypeId,
                        Name = s.Name,
                        Tests = (from t in TestsContext.Tests
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
                    }).ToList();
        }

    }
}