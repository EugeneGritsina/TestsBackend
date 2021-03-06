﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestsBackend.Models;
using TestsBackend.Repositories;
using TestsBackend.Services;

namespace TestsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize(Roles = "admin")]
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
        public ActionResult<IQueryable<TestWithObjectSubject>> GetTableOfTests()
        {
            try
            {
                return Ok(_testsRepository.GetTestsForTable());
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

        [HttpPost]
        public ActionResult Post(InputTestDTO test)
        {
            try
            {
                return Ok(_testsRepository.PostCreateTest(test));
            }
            catch(Exception e)
            {
                return BadRequest(e);
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
