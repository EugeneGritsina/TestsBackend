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
}
