using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Models.Domain;

namespace ProgrammerStudio.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<BlogTag> Tags { get; set; }
        public DbSet<BlogPostLike> Likes { get; set; }
    }
}
