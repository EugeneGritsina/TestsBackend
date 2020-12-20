using WebApiAttempt1.Models;

namespace WebApiAttempt1.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
