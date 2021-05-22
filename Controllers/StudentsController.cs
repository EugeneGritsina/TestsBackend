using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestsBackend.Models;
using TestsBackend.Repositories;
using TestsBackend.Services;

namespace TestsBackend.Controllers
{
    [Route("api/student")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "student")]
    public class StudentsController : ControllerBase
    {
        readonly ITestsRepository _testsRepository;
        readonly ITestsService _testsService;

        public StudentsController(ITestsRepository testsRepository, ITestsService testsService)
        {
            _testsRepository = testsRepository;
            _testsService = testsService;
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
        [Route("CheckAnswers")]
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
    }
}
