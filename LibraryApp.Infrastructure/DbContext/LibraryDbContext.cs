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
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }

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

            modelBuilder.Entity<Author>().ToTable("Authors");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<BookGenre>().ToTable("BookGenres");
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Message>().ToTable("Messages");


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

            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Author)
                        .WithMany(a => a.Books)
                        .HasForeignKey("AuthorId");

            modelBuilder.Entity<BookGenre>()
                        .HasOne(bg => bg.Book)
                        .WithMany(b => b.Genres)
                        .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                        .HasOne(bg => bg.Genre)
                        .WithMany(g => g.Books)
                        .HasForeignKey(bg => bg.GenreId);

            modelBuilder.Entity<Review>()
                        .HasOne(r => r.User)
                        .WithMany(u => u.Reviews)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                        .HasOne(r => r.Book)
                        .WithMany(b => b.Reviews)
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                        .HasOne(r => r.User)
                        .WithMany(u => u.Orders)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                        .HasOne(r => r.Book)
                        .WithMany(b => b.Orders)
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                        .HasOne(n => n.User)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notification>()
                        .HasOne(n => n.Book)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notification>()
                        .HasOne(n => n.NotificationReceiver)
                        .WithMany(nr => nr.Notifications)
                        .HasForeignKey("NotificationReceiverId")
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                        .HasOne(m => m.User)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
