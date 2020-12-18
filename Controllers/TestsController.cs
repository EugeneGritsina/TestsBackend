using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.Repositories;
using WebApiAttempt1.Services;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TestsController : ControllerBase
    {
        readonly ITestsRepository _testsRepository;
        readonly ITestsService _testsService;

        public TestsController(ITestsRepository testsRepository, ITestsService testsService)
        {
            _testsRepository = testsRepository;
            _testsService = testsService;
        }

        [HttpGet]
        public ActionResult<IQueryable<TestWithObjectSubject>> GetListOfSubjectAndTests()
        {
            try
            {
                return Ok(_testsRepository.GetTestsWithObjectSubject());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<TestForProfessorDTO> GetTestForProffessor(int id)
        {
            try
            {
                return Ok(_testsRepository.GetTestForProfessor(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("student/{id}")]
        public ActionResult<TestForStudentDTO> GetTestToCompleteToStudent(int id)
        {
            try
            {
                return Ok(_testsRepository.GetTestToCompleteToStudent(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(TestForProfessorDTO test)
        {
            try
            {
                return Ok(_testsRepository.SaveTest(test));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]TestForProfessorDTO test)
        {
            try
            {
                return Ok($"Test \"{_testsRepository.UpdateTest(test)}\" was updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult CloseOrOpenTest(int id)
        {
            try 
            {
                return Ok(_testsRepository.CloseOrOpenTest(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try 
            {
                _testsRepository.DeleteTest(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("student")]
        public ActionResult CheckAnswers(TestForProfessorDTO testSentByUser)
        {
            try
            {
                return Ok(_testsService.CheckAnswers(testSentByUser));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        ////запрос без фильтрации для отображения определенного количества тестов на одной конкретной странице
        //[HttpGet("{amount}/{pageNumber}")]
        //[Produces("application/json")]
        //public IQueryable<TestWithObjectSubject> GetParticularAmountOfTests(int amount, int pageNumber)
        //{
        //    return (from t in _testsContext.Tests
        //            select new TestWithObjectSubject
        //            {
        //                Id = t.Id,
        //                Name = t.Name,
        //                DueDateTime = t.DueDateTime,
        //                EstimatedTime = t.EstimatedTime,
        //                QuestionsAmount = t.QuestionsAmount,
        //                MaxMark = t.MaxMark,
        //                IsOpen = t.IsOpen,
        //                CreationDate = t.CreationDate,
        //                SubjectObject = (from s in _testsContext.Subjects
        //                                 where s.Id == t.SubjectId
        //                                 select s).First()
        //            }).Skip(amount * pageNumber).Take(amount);
        //}

       
    }
}
