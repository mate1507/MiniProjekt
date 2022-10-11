using Microsoft.EntityFrameworkCore;
using shared.Model;

namespace shared.Model
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Comment> Comments => Set<Comment>();

        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}
