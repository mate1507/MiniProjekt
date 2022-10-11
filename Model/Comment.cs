namespace Model
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
