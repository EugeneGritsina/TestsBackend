using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestsBackend.Models
{
    public class Subject
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "subject_type_id")]
        public int SubjectTypeId { get; set; }

    }
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubjectType SubjectType { get; set; }
    }
    public class SubjectsListDTO : SubjectDTO
    {
        public List<TestDTO> Tests { get; set; }
    }
}
