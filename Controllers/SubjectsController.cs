using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestsBackend.Interfaces;
using TestsBackend.Models;

namespace TestsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Produces("application/json")]
        public List<SubjectsListDTO> GetSubjectsWithListOfTests()
        {
            return _subjectsService.GetSubjectsWithListOfTests();
        }

        [HttpGet]
        [Route("list")]
        [Produces("application/json")]
        public object GetSubjects()
        {
            return _subjectsService.GetSubjects();
        }
    }
}
