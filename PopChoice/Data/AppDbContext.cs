using Microsoft.EntityFrameworkCore;
using PopChoice.Data.Entities;

namespace PopChoice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<MovieEmbedding> MovieEmbeddings => Set<MovieEmbedding>();
    }
}
