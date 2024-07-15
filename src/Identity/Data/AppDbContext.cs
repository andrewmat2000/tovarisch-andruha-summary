using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TovarischAndruha.Summary.Identity.Data;

public class AppDbContext : IdentityDbContext<AppUser> {
  public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }
}