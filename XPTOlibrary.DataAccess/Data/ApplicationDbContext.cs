using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XPTOlibrary.Models;
using XPTOlibrary.Models.ViewModels;

namespace XPTOlibrary.DataAccess;

public class ApplicationDbContext :IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    //Configure domain classes using modelBuilder here   
    //    base.OnModelCreating(modelBuilder);
    //    modelBuilder.Entity<BookCores>().HasKey(o => new { o.BookISBN, o.CoreId });

    //}
    public DbSet<Author> Author { get; set; }
    public DbSet<BookCores> BookCores { get; set; }
    public DbSet<BookInformation> BookInformation { get; set; }
    public DbSet<BorrowRecord> BorrowRecord { get; set; }
    public DbSet<Cores> Cores { get; set; }
    public DbSet<Publisher> Publisher { get; set; }
    public DbSet<Topic> Topic { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
}
