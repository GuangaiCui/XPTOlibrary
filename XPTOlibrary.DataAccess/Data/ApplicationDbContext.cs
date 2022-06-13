using Microsoft.EntityFrameworkCore;
using XPTOlibrary.Models;

namespace XPTOlibrary.DataAccess;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure domain classes using modelBuilder here   

        modelBuilder.Entity<BookAuthor>().HasKey(o => new { o.BookISBN, o.AuthorId });
        modelBuilder.Entity<BookCores>().HasKey(o => new { o.BookISBN, o.CoreId });
        modelBuilder.Entity<BookTopic>().HasKey(o => new { o.BookISBN, o.TopicId });

    }
    public DbSet<Author> Atuhor { get; set; }
    public DbSet<BookAuthor> BookAuthor { get; set; }
    public DbSet<BookCores> BookCores { get; set; }
    public DbSet<BookInformation> BookInformation { get; set; }
    public DbSet<BookTopic> BookTopic { get; set; }
    public DbSet<BorrowRecord> BorrowRecord { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<Cores> Cores { get; set; }
    public DbSet<Publisher> Publisher { get; set; }
    public DbSet<Topic> Topic { get; set; }
    public DbSet<User> User { get; set; }
}
