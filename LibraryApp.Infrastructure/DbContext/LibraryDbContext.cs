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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public LibraryDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<PostTopic>().ToTable("PostTopic");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");

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


            modelBuilder.Entity<Comment>()
                        .HasOne(c => c.User)
                        .WithMany(u => u.Comments)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                        .HasOne(c => c.Post)
                        .WithMany(p => p.Comments)
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Like>()
                        .HasOne(l => l.User)      
                        .WithMany()               
                        .HasForeignKey(l => l.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                        .HasOne(l => l.Post)
                        .WithMany(p => p.Likes)
                        .HasForeignKey(l => l.PostId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
                        .HasOne(s => s.User)
                        .WithMany()
                        .HasForeignKey(s => s.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
                        .HasOne(s => s.Post)
                        .WithMany(p => p.Saves)
                        .HasForeignKey(s => s.PostId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
                        .HasOne(s => s.Book)
                        .WithMany(b => b.Saves)
                        .HasForeignKey(s => s.BookId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                        .HasOne(s => s.User)
                        .WithMany(u => u.Subscribers)
                        .HasForeignKey(s => s.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subscription>()
                        .HasOne(s => s.Subscriber)
                        .WithMany(s => s.Subscriptions)
                        .HasForeignKey(s => s.SubscriberId)
                        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
