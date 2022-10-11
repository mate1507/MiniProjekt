namespace Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string Content { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
