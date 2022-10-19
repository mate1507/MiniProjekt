namespace shared.Model;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public User User { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public Post(User user, string title = "", int upvotes = 0, int downvotes = 0)
    {
        Title = title;
        Upvotes = upvotes;
        Downvotes = downvotes;
        User = user;
    }
    public Post()
    {
        PostId = 0;
        Title = "";
        Upvotes = 0;
        Downvotes = 0;
        User = null;
    }

    public override string ToString()
    {
        return $"PostId: {PostId}, Title: {Title}, Upvotes: {Upvotes}, Downvotes: {Downvotes}, User: {User}";
    }

}
