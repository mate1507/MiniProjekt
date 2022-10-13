using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using shared.Model;

namespace Service;

public class DataService
{
    private PostContext db { get; }

    public DataService(PostContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {
        User user = db.Users.FirstOrDefault()!;
        if (user == null)
        {
            user = new User { Username = "Kristian" };
            db.Users.Add(user);
            db.Users.Add(new User { Username = "Søren" });
            db.Users.Add(new User { Username = "Mette" });
            db.SaveChanges();
        }

        Post post = db.Posts.FirstOrDefault()!;
        if (post == null)
        {
            post = new Post { Title = "Harry Potter", User = user };
            db.Posts.Add(post);
            db.Posts.Add(new Post { Title = "Ringenes Herre", User = user });
            db.Posts.Add(new Post { Title = "Entity Framework for Dummies", User = user });
            db.SaveChanges();
        }

        Comment comment = db.Comments.FirstOrDefault()!;
        if (comment == null)
        {
            post.Comments.Add(new Comment { Text = "bedst komentar", User = user });
            post.Comments.Add(new Comment { Text = "Værste komentar", User = user });
            db.SaveChanges();
        }
    }

    public List<Post> GetPosts()
    {
        return db.Posts.Include(b => b.User).ToList();
    }

    public List<User> GetUsers()
    {
        return db.Users.ToList();
    }

    public User GetUser(int id)
    {
        return db.Users.FirstOrDefault(a => a.UserId == id);
    }

    public Post GetPost(int id)
    {
        return db.Posts
            .Include(p => p.User)
            .Include(b => b.Comments)
            .FirstOrDefault(a => a.PostId == id); //join User på en Post
    }

    public string CreatePost(string Title, int UserId)
    {
        User user = db.Users.FirstOrDefault(a => a.UserId == UserId);
        db.Posts.Add(new Post { Title = Title, User = user });
        db.SaveChanges();
        return "Post created";
    }

    public string CreateComment(string Text, int UserId, int PostId)
    {
        User user = db.Users.FirstOrDefault(a => a.UserId == UserId);
        Post post = db.Posts.FirstOrDefault(a => a.PostId == PostId);
        post.Comments.Add(new Comment { Text = Text, User = user });
        db.SaveChanges();
        return "Comment created";
    }

    public string AddUpvote(int PostId)
    {
        Post post = db.Posts.FirstOrDefault(a => a.PostId == PostId);
        post.Upvotes++;
        db.SaveChanges();
        return "Upvote added";
    }
    /*
        public string AddDownvote(int postId)
        {
            Post post = db.Posts.FirstOrDefault(a => a.PostId == postId);
            post.Downvotes--;
            db.SaveChanges();
            return "Downvote added";
        }
        */
}
