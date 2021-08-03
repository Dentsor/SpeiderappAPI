using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SpeiderappAPI.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Badge> BadgeList => Set<Badge>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<TaggedWith> TaggedWiths { get; set; } = null!;

        private IConfiguration Configuration { get; }

        public DBContext(DbContextOptions options, IConfiguration configuration) : base(options)
            => Configuration = configuration;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaggedWith>().HasKey(tw => new { tw.BadgeId, tw.TagId });

            modelBuilder.Entity<Requirement>()
                .HasMany(r => r.Requirers)
                .WithMany(r => r.Requirees)
                .UsingEntity<RequirementPrerequisite>(
                    join => join
                        .HasOne(rp => rp.Requirer)
                        .WithMany(r => r.RequirerPrerequisites)
                        .HasForeignKey(rp => rp.RequirerId),
                    join => join
                        .HasOne(rp => rp.Requiree)
                        .WithMany(rp => rp.RequireePrerequisites)
                        .HasForeignKey(rp => rp.RequireeId),
                     join => join.HasKey(rp => new { rp.RequirerId, rp.RequireeId })
                );

            modelBuilder.Seed();
        }
    }
}
