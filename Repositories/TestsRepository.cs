using System;
using System.Collections.Generic;
using System.Linq;
using WebApiAttempt1.DTO;
using WebApiAttempt1.JSONmodels;
using WebApiAttempt1.Models;
using WebApiAttempt1.ViewModels;

namespace WebApiAttempt1.Repositories
{
    public class TestsRepository : ITestsRepository
    {
        TestsContext _testsContext;
        public TestsRepository(TestsContext testsContext)
        {
            _testsContext = testsContext;
        }

        public IQueryable<TestWithObjectSubject> GetTestsWithObjectSubject()
        {
            return from t in _testsContext.Tests
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
                   };
        }

        public TestForProfessorDTO GetTestForProfessor(int id)
        {
            return (from t in _testsContext.Tests
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
                         SubjectObject = (from s in _testsContext.Subjects
                                          where s.Id == t.SubjectId
                                          select s).First(),
                         Questions = (from q in _testsContext.Questions
                                      where q.TestId == t.Id
                                      select new QuestionWithAnswers
                                      {
                                          Id = q.Id,
                                          Description = q.Description,
                                          QuestionType = q.QuestionType,
                                          Points = q.Points,
                                          Answers = (from a in _testsContext.Answers
                                                     where a.QuestionId == q.Id
                                                     select a).ToList()
                                      }).ToList()
                     }).First();
        }

        public TestForStudentDTO SendTestToCompleteToStudent(int id)
        {
            TestForStudentDTO test = (from t in _testsContext.Tests
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
                                        SubjectObject = (from s in _testsContext.Subjects
                                                        where s.Id == t.SubjectId
                                                        select s).First(),
                                        Questions = (from q in _testsContext.Questions
                                                    where q.TestId == t.Id
                                                    select new QuestionWithAnswersWithoutStatus
                                                    {
                                                        Id = q.Id,
                                                        TestId = q.TestId,
                                                        Description = q.Description,
                                                        QuestionType = q.QuestionType,
                                                        Points = q.Points,
                                                        Answers = (from a in _testsContext.Answers
                                                                    where a.QuestionId == q.Id
                                                                    select new AnswerWithoutStatus
                                                                    {
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
                        if (question.Points <= howMuchPointsLeft)
                            break;
                    }
                    if (!questions.Contains(question))
                    {
                        questions.Add(question);
                        howMuchPointsLeft -= question.Points;
                    }
                }
                test.Questions = questions;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
            }
            #endregion [REPLACING_FULL_LIST_OF_QUESTIONS_BY_AMOUNT_OF_QUESTIONS_USING_MAX_MARK]

            return test;
        }
    }
}
