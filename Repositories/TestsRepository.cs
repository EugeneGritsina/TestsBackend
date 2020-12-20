using System;
using System.Collections.Generic;
using System.Linq;
using WebApiAttempt1.DTO;
using WebApiAttempt1.DTO.InputDTO;
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
                       EstimatedTime = t.EstimatedTime,
                       QuestionsAmount = t.QuestionsAmount,
                       MaxMark = t.MaxMark,
                       IsOpen = t.IsOpen,
                       CreationDate = t.CreationDate,
                       SubjectDTO = (from s in _testsContext.Subjects
                                        where s.Id == t.SubjectId
                                        select new SubjectDTO {
                                            Id = s.Id,
                                            Name = s.Name,
                                            SubjectType = (from subType in _testsContext.SubjectTypes
                                                          where subType.Id == s.SubjectTypeId
                                                          select subType).First()
                                        }).First()
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
                         EstimatedTime = t.EstimatedTime,
                         QuestionsAmount = t.QuestionsAmount,
                         MaxMark = t.MaxMark,
                         IsOpen = t.IsOpen,
                         CreationDate = t.CreationDate,
                         SubjectObject = (from s in _testsContext.Subjects
                                          where s.Id == t.SubjectId
                                          select new SubjectDTO {
                                              Id = s.Id,
                                              Name = s.Name,
                                              SubjectType = (from st in _testsContext.SubjectTypes
                                                             where s.SubjectTypeId == st.Id
                                                            select st).First()
                                          }).First(),
                         Questions = (from q in _testsContext.Questions
                                      where q.TestId == t.Id
                                      select new QuestionWithAnswers
                                      {
                                          Id = q.Id,
                                          Description = q.Description,
                                          QuestionType = (from questionType in _testsContext.QuestionTypes
                                                           where q.QuestionTypeId == questionType.Id
                                                           select questionType).First(),
                                          Points = q.Points,
                                          Answers = (from a in _testsContext.Answers
                                                     where a.QuestionId == q.Id
                                                     select a).ToList()
                                      }).ToList()
                     }).First();
        }

        public TestForStudentDTO GetTestToCompleteToStudent(int id)
        {
            TestForStudentDTO test = new TestForStudentDTO();
            test = (from t in _testsContext.Tests
                    where t.Id == id
                    select new TestForStudentDTO
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DueDateTime = t.DueDateTime,
                        EstimatedTime = t.EstimatedTime,
                        QuestionsAmount = t.QuestionsAmount,
                        MaxMark = t.MaxMark,
                        IsOpen = t.IsOpen,
                        CreationDate = t.CreationDate,
                        SubjectDTO = (from s in _testsContext.Subjects
                                      where s.Id == t.SubjectId
                                      select new SubjectDTO
                                      {
                                          Id = s.Id,
                                          Name = s.Name,
                                          SubjectType = (from subType in _testsContext.SubjectTypes
                                                         where subType.Id == s.SubjectTypeId
                                                         select subType).First()
                                      }).First()
                    }).First();

            double howMuchLeft = test.MaxMark;

            var IdsOfQuestionsResultTestObjectAlreadyHas = new List<int>();

            while(howMuchLeft != 0) 
            {
                Question question = _testsContext.Questions.FirstOrDefault(q => q.TestId == test.Id &&
                                                                                (howMuchLeft - q.Points >= 0) &&
                                                                                IdsOfQuestionsResultTestObjectAlreadyHas.Contains(q.Id) == false);

                if (question == null)
                    break;

                test.Questions.Add(new QuestionWithAnswersWithoutStatus
                {
                    Id = question.Id,
                    TestId = question.TestId,
                    Description = question.Description,
                    QuestionType = (from qType in _testsContext.QuestionTypes
                                     where qType.Id == question.QuestionTypeId
                                     select qType).First(),
                    Points = question.Points,
                    Answers = (from a in _testsContext.Answers
                               where a.QuestionId == question.Id
                               select new AnswerWithoutStatus
                               {
                                   Id = a.Id,
                                   QuestionId = a.QuestionId,
                                   Value = a.Value
                               }).ToList()
                });

                IdsOfQuestionsResultTestObjectAlreadyHas.Append(question.Id);

                howMuchLeft -= question.Points;
            }

            return test;
        }

        public Test CreateTest(InputTestDTO test)
        {
            if (test == null)
                throw new Exception("Empty test.");

            // проверка, не создан ли тест с таким же именем, если создан - не добавлять
            if ((from t in _testsContext.Tests
                    select t.Name).Contains(test.Name))
                throw new Exception("Test with the same name already exists.");

            IEnumerable<int> alreadyUsedTestIds = from t in _testsContext.Tests
                                                  select t.Id;
            // задаем поля информации о тесте
            Test testToCreate = new Test()
            {
                // присвоение добавляемому тесту id в диапазрне от 1 до int32.MaxValue, исключая те id, которые уже имеются в БД
                Id = Enumerable.Range(1,Int32.MaxValue).First(digit => !alreadyUsedTestIds.Contains(digit)),
                Name = test.Name,
                DueDateTime = test.DueDateTime,
                EstimatedTime = test.EstimatedTime,
                QuestionsAmount = test.QuestionsAmount,
                MaxMark = test.MaxMark,
                IsOpen = test.IsOpen,
                CreationDate = DateTime.Now,
                SubjectId = test.SubjectId
            };

            _testsContext.Tests.Add(testToCreate);
            _testsContext.SaveChanges();

            //если вопросы не были переданы, не добавлять их в бд.
            if (test.Questions != null)
            {
                List<int> alreadyUsedQuestionIds = new List<int>(); 
                alreadyUsedQuestionIds = (from q in _testsContext.Questions
                                          select q.Id).ToList();
                List<int> alreadyUseвAnswerIds = new List<int>();
                alreadyUseвAnswerIds = (from a in _testsContext.Answers
                                                  select a.Id).ToList();
                foreach (var question in test.Questions)
                {
                    question.TestId = testToCreate.Id;
                    question.Id = Enumerable.Range(1, Int32.MaxValue).First(digit => !alreadyUsedQuestionIds.Contains(digit));
                    alreadyUsedQuestionIds.Add(question.Id);
                    _testsContext.Questions.Add(question);
                    _testsContext.SaveChanges();

                    if (question.Answers != null)
                    {
                        foreach (var answer in question.Answers)
                        {
                            answer.QuestionId = question.Id;
                            answer.Id = Enumerable.Range(1, Int32.MaxValue).First(digit => !alreadyUseвAnswerIds.Contains(digit));
                            alreadyUseвAnswerIds.Add(answer.Id);
                            _testsContext.Add(answer);
                            _testsContext.SaveChanges();
                        }
                    }
                }
            }
            return testToCreate;
        }

        public void DeleteTest(int id)
        {
            _testsContext.Tests.Remove(_testsContext.Tests.Find(id));
            _testsContext.SaveChanges();
        }

        public string CloseOrOpenTest(int id)
        {
            _testsContext.Tests.Find(id).IsOpen = _testsContext.Tests.Find(id).IsOpen == true ? false : true;
            _testsContext.SaveChanges();
            return $"State of test with id: {id} was changed.";
        }

        public string UpdateTest(InputTestDTO test)
        {
            Test testToUpdate = _testsContext.Tests.Find(test.Id);       // берем тест, который нужно обновить

            testToUpdate.Name = test.Name;                              // задаем поля информации о тесте
            testToUpdate.DueDateTime = test.DueDateTime;
            testToUpdate.EstimatedTime = test.EstimatedTime;
            testToUpdate.QuestionsAmount = test.QuestionsAmount;
            testToUpdate.MaxMark = test.MaxMark;
            testToUpdate.IsOpen = test.IsOpen;

            IQueryable<Question> QuestionsConnectedWithTest = from q in _testsContext.Questions
                                                              where q.TestId == test.Id
                                                              select q;
            // удаляем все старые вопросы, связанные с тестом
            _testsContext.Questions.RemoveRange(QuestionsConnectedWithTest);          

            List<int> alreadyUsedQuestionIds = new List<int>();
            alreadyUsedQuestionIds = (from q in _testsContext.Questions
                                      select q.Id).ToList();
            List<int> alreadyUseвAnswerIds = new List<int>();
            alreadyUseвAnswerIds = (from a in _testsContext.Answers
                                    select a.Id).ToList();

            // чтобы дальше их обновленную версию добавить из новой модельки теста
            foreach (var question in test.Questions)
            {
                question.TestId = test.Id;
                question.Id = Enumerable.Range(1, Int32.MaxValue).First(digit => !alreadyUsedQuestionIds.Contains(digit));

                Question questionToSave = new Question()
                {
                    Id = question.Id,
                    TestId = question.TestId,
                    Description = question.Description,
                    QuestionTypeId = question.QuestionTypeId,
                    Points = question.Points
                };

                _testsContext.Questions.Add(questionToSave);
                alreadyUsedQuestionIds.Add(question.Id);
                _testsContext.SaveChanges();
                foreach (var answer in question.Answers)
                {
                    answer.QuestionId = question.Id;
                    answer.Id = Enumerable.Range(1, Int32.MaxValue).First(digit => !alreadyUseвAnswerIds.Contains(digit));
                    alreadyUseвAnswerIds.Add(answer.Id);
                    _testsContext.Add(answer);
                    _testsContext.SaveChanges();
                }
            }

            _testsContext.SaveChanges();
            return testToUpdate.Name;
        }
    }
}
