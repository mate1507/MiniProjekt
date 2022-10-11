namespace shared.Model;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public User User { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
}