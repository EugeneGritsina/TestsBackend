using System;
using Microsoft.AspNetCore.Mvc;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.Repositories;
using WebApiAttempt1.Services;

namespace WebApiAttempt1.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        readonly ITestsRepository _testsRepository;
        readonly ITestsService _testsService;

        public StudentsController(ITestsRepository testsRepository, ITestsService testsService)
        {
            _testsRepository = testsRepository;
            _testsService = testsService;
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
