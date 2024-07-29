using System.Reflection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TovarischAndruha.Summary.Identity.Domain;

namespace TovarischAndruha.Summary.Identity.Infrastructure;

public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext> {
  public ConfigurationDbContext CreateDbContext(string[] args) {
    var migrationsAssembly = typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name;
    DbContextOptionsBuilder<ConfigurationDbContext> options = new();

    options.UseNpgsql(AppSettings.DbConnectionString, n => n.MigrationsAssembly(migrationsAssembly));

    var dbContext = new ConfigurationDbContext(options.Options);

    return dbContext;
  }
}