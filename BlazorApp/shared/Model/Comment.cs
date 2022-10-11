namespace shared.Model;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public User User { get; set; }

}
