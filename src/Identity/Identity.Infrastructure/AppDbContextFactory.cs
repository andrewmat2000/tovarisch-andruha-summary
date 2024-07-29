using AmwesProtocol.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TovarischAndruha.Summary.Identity.Domain;

namespace TovarischAndruha.Summary.Identity.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>, IDbContextOptionsBuilderAction<AppDbContext> {
  public AppDbContext CreateDbContext(string[] args) {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    GetDbContextOptionsBuilder()(optionsBuilder);

    return new AppDbContext(optionsBuilder.Options);
  }

  public Action<DbContextOptionsBuilder> GetDbContextOptionsBuilder() {
    return (options) => options.UseNpgsql(AppSettings.DbConnectionString);
  }
}