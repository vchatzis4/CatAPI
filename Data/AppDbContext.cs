using CatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CatEntity> Cats { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<CatTagEntity> CatTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatTagEntity>()
                .HasKey(ct => new { ct.CatId, ct.TagId });

            modelBuilder.Entity<CatTagEntity>()
                .HasOne(ct => ct.Cat)
                .WithMany(c => c.CatTags)
                .HasForeignKey(ct => ct.CatId);

            modelBuilder.Entity<CatTagEntity>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.CatTags)
                .HasForeignKey(ct => ct.TagId);
        }
    }
}
