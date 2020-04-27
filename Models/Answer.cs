using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAttempt1.Models
{
    public class Answer
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name:"question_id")]
        public int QuestionId { get; set; }

        [Column(name:"status")]
        public bool Status { get; set; }

        [Column(name:"value")]
        public string Value { get; set; }
    }
}
