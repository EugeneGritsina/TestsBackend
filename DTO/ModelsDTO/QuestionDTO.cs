using WebApiAttempt1.Models;

namespace WebApiAttempt1.DTO.ModelsDTO
{
    public class QuestionDTO : Question
    {
        public QuestionType QuestionType { get; set; }
    }
}
