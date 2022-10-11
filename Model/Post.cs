namespace Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostName { get; set; }

        public User User { get; set; }
    }
}
