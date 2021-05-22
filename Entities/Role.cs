using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestsBackend.Entities
{
    public class Role
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }
    }
}
