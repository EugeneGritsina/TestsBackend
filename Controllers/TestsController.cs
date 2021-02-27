using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiAttempt1.DTO;
using WebApiAttempt1.DTO.InputDTO;
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

        [HttpGet("{itemsAmount},{pageNumber}")]
        public ActionResult<IQueryable<TestWithObjectSubject>> GetListOfSubjectAndTests(int itemsAmount, int pageNumber)
        {
            try
            {
                return Ok(_testsRepository.GetTestsWithObjectSubject(itemsAmount, pageNumber));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public ActionResult Post(InputTestDTO test)
        {
            try
            {
                return Ok(_testsRepository.PostCreateTest(test));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]InputTestDTO test)
        {
            try
            {
                return Ok($"Test {_testsRepository.UpdateTest(test)} was updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult PatchCloseOrOpenTest(int id)
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
        [Route("CheckAnswers")]
        public ActionResult PostCheckAnswers(TestForProfessorDTO testSentByUser)
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
    }
}
