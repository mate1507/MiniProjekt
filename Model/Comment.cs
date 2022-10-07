namespace Model
{
    public class Comment
    {
        public string Text { get; set; }
        public User User { get; set; }
        public int CommentId { get; set; }
    }
}
