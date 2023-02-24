using Microsoft.EntityFrameworkCore;

namespace AdaptiveCourseServer.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<SchemeTask> SchemeTasks { get; set; }

        public Context(DbContextOptions<Context> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
