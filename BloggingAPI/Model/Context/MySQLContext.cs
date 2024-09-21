using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) 
            : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}
