using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAttempt1.Models
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
}
