using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestsBackend.Entities
{
    public class User
    {
        [Column(name: "id")]
        [Key]
        public int Id { get; set; }
        [Column(name: "student_ticket_id")]
        public string StudentTicketId { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "surname")]
        public string Surname { get; set; }
        
        [Column(name: "patronymic")]
        public string Patronymic { get; set; }

        [Column(name: "email")]
        public string Email { get; set; }

        [Column(name: "role")]
        public int Role { get; set; }

        [Column(name: "phone")]
        public string Phone { get; set; }

        [Column(name: "password")]
        public string Password { get; set; }
    }
    public class UserLoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}