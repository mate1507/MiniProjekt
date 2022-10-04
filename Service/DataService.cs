using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Model;

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
            db.Posts.Add(new Post { PostName = "Harry Potter", User = user });
            db.Posts.Add(new Post { PostName = "Ringenes Herre", User = user });
            db.Posts.Add(new Post { PostName = "Entity Framework for Dummies", User = user });
            db.SaveChanges();
        }

        Comment comment = db.Comments.FirstOrDefault()!;
        if (comment == null)
        {
            db.Comments.Add(new Comment { Text = "bedst komentar", User = user });
            db.Comments.Add(new Comment { Text = "Værste komentar", User = user });
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
        return db.Posts.FirstOrDefault(a => a.PostId == id);
    }

    //public Comment GetComment();

    public string CreatePost(string PostName, int UserId)
    {
        User user = db.Users.FirstOrDefault(a => a.UserId == UserId);
        db.Posts.Add(new Post { PostName = PostName, User = user });
        db.SaveChanges();
        return "Post created";
    }


}