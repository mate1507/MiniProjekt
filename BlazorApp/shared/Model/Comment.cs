namespace shared.Model;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public User User { get; set; }

    public Comment(string text = "", int upvotes = 0, int downvotes = 0, User user = null)
    {
        Text = text;
        Upvotes = upvotes;
        Downvotes = downvotes;
        User = user;
    }
    public Comment()
    {
        CommentId = 0;
        Text = "";
        Upvotes = 0;
        Downvotes = 0;
    }
}



