using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAttempt1.Models;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        TestsContext TestsContext;

        public TestsController(TestsContext testsContext)
        {
            TestsContext = testsContext;
        }

        // GET: api/Tests
        [HttpGet]
        [Produces("application/json")]
        public List<TestsListViewModel> Get()
        {
            return (from s in TestsContext.Subjects
                    select new TestsListViewModel
                    {
                        subject = s,
                        tests = (from t2 in TestsContext.Tests
                                 where t2.SubjectId == s.Id
                                 select t2).ToList()
                    }).ToList();
        }

        // GET: api/Tests/5
        [HttpGet("{id}", Name = "Get")]
        public WholeTestViewModel Get(int id)
        {
            WholeTestViewModel test = new WholeTestViewModel();
            try
            {
                // сначала запихиваем данные так, как они хранятся в таблице
                WholeTestRawModel rawTest = (from t in TestsContext.Tests
                                             where t.Id == id
                                             select new WholeTestRawModel
                                             {
                                                 Test = t,
                                                 Questions = (from q in TestsContext.Questions
                                                              where q.TestId == t.Id
                                                              select new QuestionViewModel
                                                              {
                                                                  Question = q,
                                                                  Answers = (from a in TestsContext.Answers
                                                                             where a.QuestionId == q.Id
                                                                             select a).ToList()
                                                              }).ToList()

                                             }).First();
                // изменяем поле теста
                string subjectName = (from s in TestsContext.Subjects
                                      where s.Id == id
                                      select s.Name).First();
                test.Test.SubjectName = subjectName; //здесь
                test.Test.Id = rawTest.Test.Id;
                test.Test.Name = rawTest.Test.Name;
                test.Test.MaxMark = rawTest.Test.MaxMark;
                test.Test.IsOpen = rawTest.Test.IsOpen;
                test.Test.QuestionsAmount = rawTest.Test.QuestionsAmount;
                test.Test.DueDateTime = rawTest.Test.DueDateTime;
                test.Test.EstimatedTime = rawTest.Test.EstimatedTime;
                test.Questions = rawTest.Questions;


                List<QuestionViewModel> questions = new List<QuestionViewModel>();
                double howMuchPointsLeft = test.Test.MaxMark;
                Random random = new Random();
                while (howMuchPointsLeft != 0)
                {
                    QuestionViewModel question;
                    while (true)
                    {
                        question = test.Questions[random.Next(0, test.Questions.Count)]; //извлечение произвольного вопроса из списка вопросов для теста
                        if (question.Question.Points <= howMuchPointsLeft)
                            break;
                    }
                    if (!questions.Contains(question))
                    {
                        questions.Add(question);
                        howMuchPointsLeft -= question.Question.Points;
                    }
                }
                test.Questions = questions;
            }
            catch (Exception e)
            {
                return null;
            }
            return test;
        }

        //POST: api/Tests
        [HttpPost]
        public ActionResult Post([FromBody]WholeTestCreateUpdateModel test)
        {
            if (test == null)
                return BadRequest();

            //estimatedTime приходит в формате строки, т.к. в js нет типа TimeSpan, поэтому здесь мы разбиваем эту строку на два элемента - часы и минуты
            string[] splittedEstimateTime = test.Test.EstimatedTime.Split(":");
            TimeSpan estimatedTime = new TimeSpan(int.Parse(splittedEstimateTime[0]), int.Parse(splittedEstimateTime[1]), 0);

            //конвертация объекта json в объект базы данных
            WholeTestRawModel wholeTestRawModel = new WholeTestRawModel();
            wholeTestRawModel.Test.DueDateTime = test.Test.DueDateTime;
            wholeTestRawModel.Test.EstimatedTime = estimatedTime;
            wholeTestRawModel.Test.IsOpen = test.Test.IsOpen;
            wholeTestRawModel.Test.MaxMark = test.Test.MaxMark;
            wholeTestRawModel.Test.Name = test.Test.Name;
            wholeTestRawModel.Test.QuestionsAmount = test.Test.QuestionsAmount;
            wholeTestRawModel.Test.SubjectId = (from s in TestsContext.Subjects
                                                where s.Name == test.Test.SubjectName
                                                select s.Id).First();

            if (TestsContext.Tests.Any(t => t.Name == wholeTestRawModel.Test.Name))
            {
                return BadRequest("Тест с таким именем уже существует.");
            }

            TestsContext.Tests.Add(wholeTestRawModel.Test);
            TestsContext.SaveChangesAsync();

            return Ok(test);
        }

        //[HttpPut]
        //public ActionResult Put([FromBody] WholeTestRawModel test)
        //{
        //    return Ok(test);
        //}

        [HttpPut]
        public ActionResult Put([FromBody] WholeTestRawModel id) //перегрузка метода Put(update), который открывает/закрывает   тест
        {
            try { 
                TestsContext.Tests.Find(id).IsOpen = TestsContext.Tests.Find(id).IsOpen == true ? false : true;
                TestsContext.SaveChanges();
                return Ok(id);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try 
            { 
                TestsContext.Tests.Remove(TestsContext.Tests.Find(id));
                TestsContext.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
