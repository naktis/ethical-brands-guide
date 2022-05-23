namespace Data.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public bool Approved { get; set; }

        public int EntryId { get; set; }
        public RatingEntry Entry { get; set; }
    }
}
