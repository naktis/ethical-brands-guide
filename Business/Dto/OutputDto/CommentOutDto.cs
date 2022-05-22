using Data.Models;

namespace Business.Dto.OutputDto
{
    public class CommentOutDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool? Approved { get; set; }
    }
}
