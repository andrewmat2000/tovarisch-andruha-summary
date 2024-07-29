using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Domain.Common;
using TovarischAndruha.Summary.Identity.Domain.Entities;

namespace TovarischAndruha.Summary.Identity.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser>, IAvatarContext {
  public DbSet<Avatar> Avatars { get; set; }

  public Task<int> SaveChangesAsync() {
    return base.SaveChangesAsync();
  }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
  }

  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) {
    Database.Migrate();
  }
}