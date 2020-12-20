using WebApiAttempt1.Models;

namespace WebApiAttempt1.DTO.ModelsDTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Description { get; set; }
        public double Points { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
