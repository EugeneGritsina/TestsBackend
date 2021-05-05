using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsBackend.JSONmodels;
using TestsBackend.Models;
using TestsBackend.ViewModels;

namespace TestsBackend.Services
{
    public class TestsService : ITestsService
    {
        TestsContext _testsContext;

        public TestsService(TestsContext testsContext)
        {
            _testsContext = testsContext;
        }

        public float CheckAnswers(TestForProfessorDTO testSentByUser)
        {
            float gainedMark = 0;

            foreach (QuestionWithAnswers q in testSentByUser.Questions)
            {
                List<Answer> correctAnswersForTheQuestion = new List<Answer>();
                correctAnswersForTheQuestion = (from a in _testsContext.Answers
                                                where a.QuestionId == q.Id && a.Status == true
                                                select a).ToList();

                bool isAnsweredRight = true;

                if (correctAnswersForTheQuestion.Count == q.Answers.Count)
                    foreach (Answer studentAnswer in q.Answers)
                    {
                        // если список правильных ответов не содержит данного ответа, то он неправильный
                        if (!correctAnswersForTheQuestion.Exists(answer => answer.Id == studentAnswer.Id))
                        {
                            isAnsweredRight = false;
                        }
                    }
                else
                    isAnsweredRight = false;
                    
                if (isAnsweredRight)
                    gainedMark += (float)q.Points;
            }
            return gainedMark;
        }

        public bool UpdateStatusOfTestsAccordingToCurrentNow()
        {
            
            return true;
        }
    }
}
