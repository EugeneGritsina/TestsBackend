using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
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


        [HttpGet]
        [Produces("application/json")]
        public IQueryable<TestWithObjectSubject> Get()
        {
            return from t in TestsContext.Tests
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
                        SubjectObject = (from s in TestsContext.Subjects
                                         where s.Id == t.SubjectId
                                         select s).First()
                    };
        }

        [HttpGet("{id}", Name = "Get")]
        [Produces("application/json")]
        public ActionResult<TestForProfessorDTO> Get(int id)
        {
            TestForProfessorDTO test = new TestForProfessorDTO();
            try
            {
                test = (from t in TestsContext.Tests
                        where t.Id == id
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
                            SubjectObject = (from s in TestsContext.Subjects
                                             where s.Id == t.SubjectId
                                             select s).First(),
                            Questions = (from q in TestsContext.Questions
                                         where q.TestId == t.Id
                                         select new QuestionWithAnswers
                                         {
                                             Question = q,
                                             Answers = (from a in TestsContext.Answers
                                                        where a.QuestionId == q.Id
                                                        select a).ToList()
                                         }).ToList()
                        }).First();
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
        public ActionResult<TestForStudentDTO> GetTest(int id)
        {
            TestForStudentDTO test = new TestForStudentDTO();
            try
            {
                test = (from t in TestsContext.Tests
                        where t.Id == id
                        select new TestForStudentDTO
                        {
                            Id = t.Id,
                            Name = t.Name,
                            DueDateTime = t.DueDateTime,
                            EstimatedTime = $"{t.EstimatedTime.Value.Hours}:{t.EstimatedTime.Value.Minutes}",
                            QuestionsAmount = t.QuestionsAmount,
                            MaxMark = t.MaxMark,
                            IsOpen = t.IsOpen,
                            CreationDate = t.CreationDate,
                            SubjectObject = (from s in TestsContext.Subjects
                                            where s.Id == t.SubjectId
                                            select s).First(),
                            Questions = (from q in TestsContext.Questions
                                        where q.TestId == t.Id
                                        select new QuestionWithAnswersWithoutStatus
                                        {
                                            Question = q,
                                            Answers = (from a in TestsContext.Answers
                                                        where a.QuestionId == q.Id
                                                        select new AnswerWithoutStatus { 
                                                            Id = a.Id,
                                                            QuestionId = a.QuestionId,
                                                            Value = a.Value
                                                        }).ToList()
                                        }).ToList()
                        }).First();

                #region [REPLACING_FULL_LIST_OF_QUESTIONS_BY_AMOUNT_OF_QUESTIONS_USING_MAX_MARK]
                List<QuestionWithAnswersWithoutStatus> questions = new List<QuestionWithAnswersWithoutStatus>();
                double howMuchPointsLeft = test.MaxMark;
                Random random = new Random();
                try
                {
                    while (howMuchPointsLeft != 0)
                    {
                        QuestionWithAnswersWithoutStatus question;
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
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e);
                }
                #endregion [REPLACING_FULL_LIST_OF_QUESTIONS_BY_AMOUNT_OF_QUESTIONS_USING_MAX_MARK]}
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
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
            foreach (var t in TestsContext.Tests)
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
                TestsContext.Tests.Add(testToCreate);
                TestsContext.SaveChanges();

                foreach (var q in test.Questions)                                           // чтобы дальше их обновленную версию добавить из новой модельки теста
                {
                    q.Question.TestId = testToCreate.Id;
                    q.Question.Id = 0;                                                          // в БД уже может быть вопрос с таким ID, поэтому возникнет ошибка
                    TestsContext.Questions.Add(q.Question);                                     // для решения проблемы ID зануляется, вопрос добавляется в БД, а всем связанным ответам
                    TestsContext.SaveChanges();
                    foreach (var a in q.Answers)                                                // присваивается новое значение ID, выданное БД
                        a.QuestionId = q.Question.Id;
                }

                foreach (var q in test.Questions)                                           // и после этого добавляем также ответы
                {
                    foreach (var a in q.Answers)
                        a.Id = 0;
                    TestsContext.Answers.AddRange(q.Answers);
                }

                TestsContext.SaveChanges();

                // в принципе, без этого запроса можно было бы и обойтись, я думаю, просто можно было переприсвоить id самого теста, а что там с остальными id -- вроде, не важно
                test = (from t in TestsContext.Tests
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
                            SubjectObject = (from s in TestsContext.Subjects
                                             where s.Id == t.SubjectId
                                             select s).First(),
                            Questions = (from q in TestsContext.Questions
                                         where q.TestId == t.Id
                                         select new QuestionWithAnswers
                                         {
                                             Question = q,
                                             Answers = (from a in TestsContext.Answers
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
                    correctAnswersFor_q = (from a in TestsContext.Answers
                                           where a.QuestionId == q.Question.Id && a.Status == true
                                           select a).ToList();

                    if (correctAnswers == q.Answers)
                        gainedMark += (float)q.Question.Points;

                    correctAnswers.AddRange(correctAnswersFor_q);
                }


                return Ok();
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

                Test testToUpdate = TestsContext.Tests.Find(test.Id);       // берем тест, который нужно обновить

                testToUpdate.Name = test.Name;                              // задаем поля информации о тесте
                testToUpdate.DueDateTime = test.DueDateTime;
                testToUpdate.EstimatedTime = new TimeSpan(int.Parse(splittedEstimatedTime[0]), int.Parse(splittedEstimatedTime[1]), 0);
                testToUpdate.QuestionsAmount = test.QuestionsAmount;
                testToUpdate.MaxMark = test.MaxMark;
                testToUpdate.IsOpen = test.IsOpen;

                int[] IdsOfQuestionsConnectedWithTest = (from q in TestsContext.Questions      //сохраняем Id вопросов, которые связаны с тестом, чтобы потом по этим id найти ответы, связанные с нужными вопросами 
                                                         where q.TestId == test.Id
                                                         select q.Id).ToArray();

                List<Answer> AnswersConnectedWithTest = new List<Answer>();                 //создаем и инициализируем список всех ответов, связанных с тестом

                for (int i = 0; i < IdsOfQuestionsConnectedWithTest.Length; i++)
                {
                    TestsContext.Answers.RemoveRange((from a in TestsContext.Answers
                                                      where a.QuestionId == IdsOfQuestionsConnectedWithTest[i]
                                                      select a).ToList());
                }

                TestsContext.Answers.RemoveRange(AnswersConnectedWithTest);                 //очищаем список всех ответов от нужных ответов

                var QuestionsConnectedWithTest = from q in TestsContext.Questions
                                                 where q.TestId == test.Id
                                                 select q;
                TestsContext.Questions.RemoveRange(QuestionsConnectedWithTest);             // стираем все вопросы...

                foreach (var q in test.Questions)                                           // чтобы дальше их обновленную версию добавить из новой модельки теста
                {
                    q.Question.Id = 0;                                                          // в БД уже может быть вопрос с таким ID, поэтому возникнет ошибка
                    TestsContext.Questions.Add(q.Question);                                     // для решения проблемы ID зануляется, вопрос добавляется в БД, а всем связанным ответам
                    TestsContext.SaveChanges();
                    foreach (var a in q.Answers)                                                // присваивается новое значение ID, выданное БД
                        a.QuestionId = q.Question.Id;
                }

                foreach (var q in test.Questions)                                           // и после этого добавляем также ответы
                {
                    foreach (var a in q.Answers)
                        a.Id = 0;
                    TestsContext.Answers.AddRange(q.Answers);
                }

                TestsContext.SaveChanges();
                return Ok(test);
            }
            catch
            {
                return BadRequest();
            }
        }

        //перегрузка метода Put(update), который открывает/закрывает тест
        [HttpPut("{id}")]
        public ActionResult Put(int id)
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




        //запрос без фильтрации для отображения определенного количества тестов на одной конкретной странице
        [HttpGet("{amount}/{pageNumber}")]
        [Produces("application/json")]
        public IQueryable<TestWithObjectSubject> GetParticularAmountOfTests(int amount, int pageNumber)
        {
            return (from t in TestsContext.Tests
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
                        SubjectObject = (from s in TestsContext.Subjects
                                         where s.Id == t.SubjectId
                                         select s).First()
                    }).Skip(amount * pageNumber).Take(amount);
        }

        // GET: api/Tests
        //[HttpGet]
        //[Produces("application/json")]
        //public List<SubjectsListViewModel> GetSubjects()
        //{
        //    return (from s in TestsContext.Subjects
        //            select new SubjectsListViewModel
        //            {
        //                Id = s.Id,
        //                SubjectTypeId = s.SubjectTypeId,
        //                Name = s.Name,
        //                Tests = (from t2 in TestsContext.Tests
        //                         where t2.SubjectId == s.Id
        //                         select t2).ToList()
        //            }).ToList();
        //}
    }
}
