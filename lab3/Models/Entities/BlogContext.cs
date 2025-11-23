using Microsoft.EntityFrameworkCore;

namespace lab3.Models.Entities;

public class BlogContext: DbContext
{
  public DbSet<BlogArticle> BlogArticles { get; set; }
  
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=blog.db");
  }
}