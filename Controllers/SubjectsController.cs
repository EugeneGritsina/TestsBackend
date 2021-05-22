using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestsBackend.Interfaces;
using TestsBackend.Models;

namespace TestsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SubjectsController : ControllerBase
    {
        ISubjectsService _subjectsService;
        public SubjectsController(ISubjectsService subjectsService) => _subjectsService = subjectsService;

        //[HttpGet("{itemsAmount}, {pageNumber}")]
        //[Produces("application/json")]
        //public List<SubjectsListDTO> GetSubjectsWithListOfTests(int itemsAmount, int pageNumber)
        //{
        //    return _subjectsService.GetSubjectsWithListOfTests(itemsAmount, pageNumber);
        //}

        [HttpGet]
        public ActionResult<List<SubjectsListDTO>> GetSubjectsWithListOfTests()
        {
            try
            {
                return _subjectsService.GetSubjectsWithListOfTests();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<List<SubjectDTO>> GetSubjects()
        {
            try
            {
                return _subjectsService.GetSubjects();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
