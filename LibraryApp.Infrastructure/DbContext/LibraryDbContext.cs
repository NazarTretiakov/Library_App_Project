using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.DbContext
{
    public class LibraryDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<PostTopic> PostTopic { get; set; }

        public LibraryDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<PostTopic>().ToTable("PostTopic");

            modelBuilder.Entity<Like>().ToTable("Likes");
            modelBuilder.Entity<Save>().ToTable("Saves");


            modelBuilder.Entity<User>()
                        .HasMany(u => u.Posts)
                        .WithOne(p => p.User)
                        .HasForeignKey("UserId");

            modelBuilder.Entity<PostTopic>()
                        .HasOne(pt => pt.Post)
                        .WithMany(p => p.Topics)
                        .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTopic>()
                        .HasOne(pt => pt.Topic)
                        .WithMany(t => t.Posts)
                        .HasForeignKey(pt => pt.TopicId);


            modelBuilder.Entity<Like>()
                        .HasOne(l => l.User)      
                        .WithMany()               
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                        .HasOne(l => l.Post)
                        .WithMany(p => p.Likes)
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
                        .HasOne(s => s.User)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
                        .HasOne(s => s.Post)
                        .WithMany(p => p.Saves)
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
