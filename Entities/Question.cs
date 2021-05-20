using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestsBackend.Models
{
    public class Question
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name: "test_id")]
        public int TestId { get; set; }

        [Column(name: "description")]
        public string Description { get; set; }

        [Column(name: "type_id")]
        public int QuestionTypeId { get; set; }

        [Column(name: "points")]
        public double Points { get; set; }
    }
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Description { get; set; }
        public double Points { get; set; }
        public QuestionType QuestionType { get; set; }
    }

    public class InputQuestionWithAnswers : Question
    {
        public List<Answer> Answers { get; set; }

        public InputQuestionWithAnswers()
        {
            Answers = new List<Answer>();
        }
    }

    public class QuestionWithAnswers : QuestionDTO
    {
        public List<Answer> Answers { get; set; }

        public QuestionWithAnswers()
        {
            Answers = new List<Answer>();
        }
    }
}
