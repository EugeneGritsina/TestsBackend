using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestsBackend.Models
{
    public class QuestionType
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name: "type")]
        public string Type { get; set; }
    }
}
