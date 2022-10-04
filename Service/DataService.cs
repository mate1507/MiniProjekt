using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Model;

namespace Service;

public class DataService
{
    private PostContext db { get; }

    public DataService(PostContext db) {
        this.db = db;
    }
    /*
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData() {
        
        User user = db.Users.FirstOrDefault()!;
        if (user == null) {
            user = new User { Username = "Kristian" };
            db.Users.Add(author);
            db.Users.Add(new User { Username = "Søren" });
            db.Users.Add(new User { Username = "Mette" });
        }

        Post post = db.Posts.FirstOrDefault()!;
        if (post == null)
        {
            db.Posts.Add(new Post { PostName = "Harry Potter", User = user });
            db.Posts.Add(new Post { PostName = "Ringenes Herre", User = user });
            db.Posts.Add(new Post { PostName = "Entity Framework for Dummies", User = user });
        }

        db.SaveChanges();
    }

    public List<Post> GetPosts() {
        return db.Posts.Include(b => b.User).ToList();
    }

    public Book GetPost(int id) {
        return db.Posts.Include(b => b.User).FirstOrDefault(b => b.PostId == id);
    }

    public List<User> GetUsers() {
        return db.Users.ToList();
    }

    public User GetUser(int id) {
        return db.Users.Include(a => a.Posts).FirstOrDefault(a => a.UserId == id);
    }

    public List<Comment> GetComments(){
        return db.Comments.ToList();
    }

    public Comment GetComment()

    public string CreatePost(string PostName, int UserId) {
        User user = db.Users.FirstOrDefault(a => a.UserId == userId);
        db.Posts.Add(new Post { PostName = postname, User = user });
        db.SaveChanges();
        return "Post created";
    }
    */

}