using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestsBackend.Models
{
    public class Test
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name: "subject_id")]
        public int SubjectId { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "due_date_time")]
        public DateTime? DueDateTime { get; set; }

        [Column(name: "estimated_time")]
        public DateTime? EstimatedTime { get; set; }

        [Column(name: "questions_amount")]
        public int QuestionsAmount { get; set; }

        [Column(name: "max_mark")]
        public int MaxMark { get; set; }

        [Column(name: "is_open")]
        public bool IsOpen { get; set; }

        [Column(name: "creation_date")]
        public DateTime CreationDate { get; set; }
    }

    public class TestTableModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string SubjectName { get; set; }
    }
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDateTime { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public int QuestionsAmount { get; set; }
        public int MaxMark { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class InputTestDTO : Test
    {
        public List<InputQuestionWithAnswers> Questions { get; set; }

        public InputTestDTO()
        {
            Questions = new List<InputQuestionWithAnswers>();
        }
    }

    public class CompletedTestDTO : TestDTO
    {
        public int GainedMark { get; set; }
    }

    //<description> Test with all connected questions and question answers have status here. </description>
    public class TestForProfessorDTO : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
        public List<QuestionWithAnswers> Questions { get; set; }
    }

    public class TestForStudentDTO : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
        public List<QuestionWithAnswersWithoutStatus> Questions { get; set; }

        public TestForStudentDTO()
        {
            SubjectDTO = new SubjectDTO();
            Questions = new List<QuestionWithAnswersWithoutStatus>();
        }
    }

    public class AnswerWithoutStatus
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Value { get; set; }
    }

    public class QuestionWithAnswersWithoutStatus : QuestionDTO
    {
        public List<AnswerWithoutStatus> Answers { get; set; }

        public QuestionWithAnswersWithoutStatus()
        {
            Answers = new List<AnswerWithoutStatus>();
        }
    }
    public class TestWithObjectSubject : TestDTO
    {
        public SubjectDTO SubjectDTO { get; set; }
    }
}
