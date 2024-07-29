using System.Reflection;
using AmwesProtocol.Infrastructure.Common;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TovarischAndruha.Summary.Identity.Domain;

namespace TovarischAndruha.Summary.Identity.Infrastructure;

public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext> {
  public PersistedGrantDbContext CreateDbContext(string[] args) {
    var migrationsAssembly = typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name;
    DbContextOptionsBuilder<PersistedGrantDbContext> options = new();

    options.UseNpgsql(AppSettings.DbConnectionString, n => n.MigrationsAssembly(migrationsAssembly));

    var dbContext = new PersistedGrantDbContext(options.Options);

    return dbContext;
  }
}