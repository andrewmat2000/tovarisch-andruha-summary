using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Data;

namespace TovarischAndruha.Summary.Identity.Extensions;

public static class SeedDbExtension {
  public static WebApplication SeedDb(this WebApplication webApplication) {
    using var scope = webApplication.Services.CreateScope();

    using var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
    persistedGrantDbContext.Database.Migrate();

    using var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    configurationDbContext.Database.Migrate();

    if (!configurationDbContext.ApiScopes.Any()) {
      foreach (var apiScope in IdentityConfig.ApiScopes) {
        configurationDbContext.ApiScopes.Add(apiScope);
      }
    }

    if (!configurationDbContext.ApiResources.Any()) {
      foreach (var apiResource in IdentityConfig.ApiResources) {
        configurationDbContext.ApiResources.Add(apiResource);
      }
    }

    if (!configurationDbContext.IdentityResources.Any()) {
      foreach (var identityResource in IdentityConfig.IdentityResources) {
        configurationDbContext.IdentityResources.Add(identityResource);
      }
    }

    if (!configurationDbContext.Clients.Any()) {
      foreach (var client in IdentityConfig.Clients) {
        configurationDbContext.Clients.Add(client);
      }
    }

    configurationDbContext.SaveChanges();

    using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    return webApplication;
  }
}