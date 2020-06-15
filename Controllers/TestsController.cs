using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.Models;
using WebApiAttempt1.Repositories;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        readonly ITestsRepository _testsRepository;
        TestsContext _testsContext;

        public TestsController(TestsContext testsContext, ITestsRepository testsRepository)
        {
            _testsContext = testsContext;
            _testsRepository = testsRepository;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IQueryable<TestWithObjectSubject>> Get()
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
        [Produces("application/json")]
        public ActionResult<TestForProfessorDTO> Get(int id)
        {
            TestForProfessorDTO test;
            try
            {
                test = _testsRepository.GetTestForProfessor(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok(test);
        }

        [HttpGet]
        [Route("student/{id}")]
        [Produces("application/json")]
        public ActionResult<TestForStudentDTO> SendTestToCompleteToStudent(int id)
        {
            TestForStudentDTO test;
            try
            {
                test = _testsRepository.SendTestToCompleteToStudent(id);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            return Ok(test);
        }

        [HttpPost]
        public ActionResult Post(TestForProfessorDTO test)
        {
            if (test == null)
                return BadRequest();

            List<string> testNames = new List<string>();
            foreach (var t in _testsContext.Tests)
                testNames.Add(t.Name);
            if (testNames.Contains(test.Name))                                               // проверка, не создан ли тест с таким же именем, если создан - не добавлять
                return BadRequest("Test with the same name already exists.");

            try
            {
                string[] splittedEstimatedTime = test.EstimatedTime.Split(':');
                // задаем поля информации о тесте
                Test testToCreate = new Test() {
                    Id = 0,
                    Name = test.Name,
                    DueDateTime = test.DueDateTime,
                    EstimatedTime = new TimeSpan(int.Parse(splittedEstimatedTime[0]),int.Parse(splittedEstimatedTime[1]), 0),
                    QuestionsAmount = test.QuestionsAmount,
                    MaxMark = test.MaxMark,
                    IsOpen = test.IsOpen,
                    CreationDate = DateTime.Now,
                    SubjectId = test.SubjectObject.Id
                };
                _testsContext.Tests.Add(testToCreate);
                _testsContext.SaveChanges();

                if (test.Questions != null) //если не были переданы вопросы, не добавлять их в бд.
                {

                    foreach (var q in test.Questions)                                           // чтобы дальше их обновленную версию добавить из новой модельки теста
                    {
                        q.TestId = testToCreate.Id;
                        q.Id = 0;                                                          // в БД уже может быть вопрос с таким ID, поэтому возникнет ошибка
                        _testsContext.Questions.Add(q);                                     // для решения проблемы ID зануляется, вопрос добавляется в БД, а всем связанным ответам
                        _testsContext.SaveChanges();
                        foreach (var a in q.Answers)                                                // присваивается новое значение ID, выданное БД
                            a.QuestionId = q.Id;
                    }

                    foreach (var q in test.Questions)                                           // и после этого добавляем также ответы
                    {
                        if (q.Answers != null) { 
                            foreach (var a in q.Answers)
                                a.Id = 0;
                            _testsContext.Answers.AddRange(q.Answers);
                        }
                    }
                    _testsContext.SaveChanges();
                }

                // в принципе, без этого запроса можно было бы и обойтись, я думаю, просто можно было переприсвоить id самого теста, а что там с остальными id -- вроде, не важно
                test = (from t in _testsContext.Tests
                        where t.Id == testToCreate.Id
                        select new TestForProfessorDTO
                        {
                            Id = t.Id,
                            Name = t.Name,
                            DueDateTime = t.DueDateTime,
                            EstimatedTime = $"{t.EstimatedTime.Value.Hours}:{t.EstimatedTime.Value.Minutes}",
                            QuestionsAmount = t.QuestionsAmount,
                            MaxMark = t.MaxMark,
                            IsOpen = t.IsOpen,
                            CreationDate = t.CreationDate,
                            SubjectObject = (from s in _testsContext.Subjects
                                             where s.Id == t.SubjectId
                                             select s).First(),
                            Questions = (from q in _testsContext.Questions
                                         where q.TestId == t.Id
                                         select new QuestionWithAnswers
                                         {
                                             Id = q.Id,
                                             TestId = q.TestId,
                                             Description = q.Description,
                                             QuestionType = q.QuestionType,
                                             Points = q.Points,
                                             Answers = (from a in _testsContext.Answers
                                                        where a.QuestionId == q.Id
                                                        select a).ToList()
                                         }).ToList()
                        }).First();

                return Ok(test);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]TestForProfessorDTO test)
        {
            try
            {
                string[] splittedEstimatedTime = test.EstimatedTime.Split(':');

                Test testToUpdate = _testsContext.Tests.Find(test.Id);       // берем тест, который нужно обновить

                testToUpdate.Name = test.Name;                              // задаем поля информации о тесте
                testToUpdate.DueDateTime = test.DueDateTime;
                testToUpdate.EstimatedTime = new TimeSpan(int.Parse(splittedEstimatedTime[0]), int.Parse(splittedEstimatedTime[1]), 0);
                testToUpdate.QuestionsAmount = test.QuestionsAmount;
                testToUpdate.MaxMark = test.MaxMark;
                testToUpdate.IsOpen = test.IsOpen;
                                
                IQueryable<Question> QuestionsConnectedWithTest = from q in _testsContext.Questions      
                                                                       where q.TestId == test.Id
                                                                       select q;

                _testsContext.Questions.RemoveRange(QuestionsConnectedWithTest);             // удаляем все старые вопросы, связанные с тестом
                
                foreach (var q in test.Questions)                                           // чтобы дальше их обновленную версию добавить из новой модельки теста
                {
                    q.Id = 0;                                                          // в БД уже может быть вопрос с таким ID, поэтому возникнет ошибка
                    _testsContext.Questions.Add(q);                                     // для решения проблемы ID зануляется, вопрос добавляется в БД, а всем связанным ответам
                    _testsContext.SaveChanges();
                    foreach (var a in q.Answers)                                                // присваивается новое значение ID, выданное БД
                        a.QuestionId = q.Id;
                }

                foreach (var q in test.Questions)                                           // и после этого добавляем также ответы
                {
                    foreach (var a in q.Answers)
                        a.Id = 0;
                    _testsContext.Answers.AddRange(q.Answers);
                }

                _testsContext.SaveChanges();
                return Ok(test);
            }
            catch
            {
                return BadRequest();
            }
        }

        // метод открывает/закрывает тест
        [HttpPatch("{id}")]
        public ActionResult CloseOrOpenTest(int id)
        {
            try { 
                _testsContext.Tests.Find(id).IsOpen = _testsContext.Tests.Find(id).IsOpen == true ? false : true;
                _testsContext.SaveChanges();
                return Ok($"State of test with id: {id} was changed.");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try 
            { 
                _testsContext.Tests.Remove(_testsContext.Tests.Find(id));
                _testsContext.SaveChanges();
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
            if (testSentByUser == null)
                return BadRequest();
            try
            {
                float gainedMark = 0;

                List<Answer> correctAnswers = new List<Answer>();

                foreach (QuestionWithAnswers q in testSentByUser.Questions)
                {
                    List<Answer> correctAnswersFor_q = new List<Answer>();
                    correctAnswersFor_q = (from a in _testsContext.Answers
                                           where a.QuestionId == q.Id && a.Status == true
                                           select a).ToList();

                    if (correctAnswers == q.Answers)
                        gainedMark += (float)q.Points;

                    correctAnswers.AddRange(correctAnswersFor_q);
                }


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //запрос без фильтрации для отображения определенного количества тестов на одной конкретной странице
        [HttpGet("{amount}/{pageNumber}")]
        [Produces("application/json")]
        public IQueryable<TestWithObjectSubject> GetParticularAmountOfTests(int amount, int pageNumber)
        {
            return (from t in _testsContext.Tests
                    select new TestWithObjectSubject
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DueDateTime = t.DueDateTime,
                        EstimatedTime = $"{t.EstimatedTime.Value.Hours}:{t.EstimatedTime.Value.Minutes}",
                        QuestionsAmount = t.QuestionsAmount,
                        MaxMark = t.MaxMark,
                        IsOpen = t.IsOpen,
                        CreationDate = t.CreationDate,
                        SubjectObject = (from s in _testsContext.Subjects
                                         where s.Id == t.SubjectId
                                         select s).First()
                    }).Skip(amount * pageNumber).Take(amount);
        }

       
    }
}
