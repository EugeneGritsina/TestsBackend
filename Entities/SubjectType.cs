using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAttempt1.Models
{
    public class SubjectType
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name: "type")]
        public string Type { get; set; }
    }
}