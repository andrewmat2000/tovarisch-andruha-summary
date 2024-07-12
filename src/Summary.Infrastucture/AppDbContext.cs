using Microsoft.EntityFrameworkCore;
using Summary.Domain;
using Summary.Domain.Models;
using Summary.Persistence.Interfaces;

namespace Summary.Infrastucture;

public class AppDbContext : DbContext, IArticleDbContext, IUnitOfWork {
  private readonly AppConfiguration _options = new();
  public DbSet<Article> Articles { get; set; } = null!;
  public async Task<int> SaveChangesAsync() {
    return await base.SaveChangesAsync();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    optionsBuilder.UseNpgsql(_options.DbConnectionString);
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }

  public AppDbContext() {
    Database.Migrate();
  }
}