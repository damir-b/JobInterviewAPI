using JobInterviewAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobInterviewAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
