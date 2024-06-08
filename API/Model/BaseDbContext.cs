using API.Model.Core;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
                
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoCategory> TodoCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Todo.Config().Configure(modelBuilder.Entity<Todo>());
            new TodoCategory.Config().Configure(modelBuilder.Entity<TodoCategory>());
        }
    }
}
