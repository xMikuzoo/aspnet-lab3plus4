using Microsoft.EntityFrameworkCore;

namespace lab3.Models.Entities;

public class BlogContext: DbContext
{
  public DbSet<BlogArticle> Articles { get; set; }
  public DbSet<BlogComment> Comments { get; set; }
  
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=blog.db");
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<BlogArticle>()
      .HasKey(x => x.Id);
    modelBuilder.Entity<BlogArticle>()
      .HasMany(x => x.Comments)
      .WithOne(x => x.Article)
      .HasForeignKey(x => x.ArticleId )
      .OnDelete(DeleteBehavior.Cascade);
    modelBuilder.Entity<BlogComment>()
      .HasKey(x => x.Id);
  }
}