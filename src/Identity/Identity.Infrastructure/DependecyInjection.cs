using System.Reflection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Domain;
using TovarischAndruha.Summary.Identity.Domain.Entities;
using TovarischAndruha.Summary.Identity.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection {
  public static IServiceCollection AddMyIdentity(this IServiceCollection services) {
    services.AddDbContext<AppDbContext>(b => b.UseNpgsql(AppSettings.DbConnectionString));

    services.AddIdentity<AppUser, IdentityRole>(x => {
      x.Password.RequireNonAlphanumeric = false;
      x.Password.RequireDigit = false;
      x.Password.RequireUppercase = false;
      x.User.RequireUniqueEmail = true;
    })
      .AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders();

    return services;
  }
  /// <summary>
  ///
  /// </summary>
  /// <typeparam name="T">T is app class for getting assembly.</typeparam>
  /// <param name="services"></param>
  /// <returns></returns>
  public static IIdentityServerBuilder AddDbInfrastructure(this IIdentityServerBuilder services) {
    var migrationsAssembly = typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name;

    services.AddAspNetIdentity<AppUser>()
            .AddConfigurationStore(builder =>
              builder.ConfigureDbContext = b =>
                b.UseNpgsql(AppSettings.DbConnectionString, n => n.MigrationsAssembly(migrationsAssembly))
            )
            .AddOperationalStore(builder =>
              builder.ConfigureDbContext = b =>
                b.UseNpgsql(AppSettings.DbConnectionString, n => n.MigrationsAssembly(migrationsAssembly))
            );

    return services;
  }
  /// <summary>
  /// Заполняет БД данными из класса IdentityConfig.
  /// </summary>
  /// <param name="app"></param>
  /// <exception cref="Exception"></exception>
  public static WebApplication EnsureSeedData(this WebApplication app) {
    using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
    persistedGrantDbContext.Database.Migrate();

    var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    context.Database.Migrate();

    if (!context.Clients.Any()) {
      foreach (var client in IdentityConfig.GetClients()) {
        context.Clients.Add(client.ToEntity());
      }
      context.SaveChanges();
    }

    if (!context.IdentityResources.Any()) {
      foreach (var resource in IdentityConfig.GetIdentityResources()) {
        context.IdentityResources.Add(resource.ToEntity());
      }
      context.SaveChanges();
    }

    if (!context.ApiScopes.Any()) {
      foreach (var resource in IdentityConfig.GetApiScopes()) {
        context.ApiScopes.Add(resource.ToEntity());
      }
      context.SaveChanges();
    }

    var appDbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbcontext.Database.Migrate();

    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    var hasAvatars = appDbcontext.Avatars.Any();

    foreach (var user in IdentityConfig.GetUsers()) {
      if (user.Avatar is Avatar avatar && !hasAvatars) {
        appDbcontext.Add(avatar);
      }
      var foundUser = userMgr.FindByNameAsync(user.UserName).Result;
      if (foundUser == null) {
        foundUser = user;

        var result = userMgr.CreateAsync(foundUser, user.Password).Result;

        if (!result.Succeeded) {
          throw new Exception(result.Errors.First().Description);
        }

        result = userMgr.AddClaimsAsync(foundUser, user.Claims).Result;
        if (!result.Succeeded) {
          throw new Exception(result.Errors.First().Description);
        }

        if (!hasAvatars) {
          appDbcontext.SaveChanges();
        }

        app.Logger.LogInformation($"User {user.UserName} created.");
      } else {
        app.Logger.LogInformation($"User {user.UserName} already exists.");
      }
    }

    return app;
  }
}

