using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace TovarischAndruha.Summary.Identity.Domain;

/// <summary>
/// Конфиг для работы IdentityServer4 в режиме разработки.
/// </summary>
public static class IdentityConfig {
  public static IEnumerable<IdentityResource> GetIdentityResources() {
    // определяет, какие scopes будут доступны IdentityServer
    return new List<IdentityResource> {
        // "sub" claim
        // стандартные claims в соответствии с profile scope
        // http://openid.net/specs/openid-connect-core-1_0.html#ScopeClaims
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };
  }
  public static IEnumerable<ApiResource> GetApiResources() {
    // claims этих scopes будут включены в access_token
    return new List<ApiResource> {
      // определяем scope "api1" для IdentityServer
      new ApiResource("api1", "API 1",
      // эти claims войдут в scope api1
      new[] { "name", "role" })
    };
  }
  public static IEnumerable<Client> GetClients() {
    return new List<Client> {
        new() {
          ClientId = "webapp",
          AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //основной сценарий входа
          RequireClientSecret = false, //Client Secret в браузере не понадобится, выключаем
          AllowedScopes = {
            "api",
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile
          },//для получения инфы о пользователе по /connect/userinfo
          AllowOfflineAccess = true, //включает рефреш-токен
          AllowedCorsOrigins = { AppSettings.HostUrl }
        }
    };
  }
  public static List<TestAppUser> GetUsers() {
    return new List<TestAppUser> {
      new() {
        DisplayName = "admin",
        UserName = "admin",
        Email = "admin@admin.ru",
        Password = AppSettings.DefaultUserPassword,

        Claims = new [] {
          new Claim("name", "Bob"),
          new Claim("website", "https://bob.com"),
          new Claim("role", "admin"),
        },
        Avatar = new() {
          Extension = string.Format("image/{0}", AppSettings.DefaultUserAvatarPath.Split('.').Last()),
          Photo = File.ReadAllBytes(AppSettings.DefaultUserAvatarPath),
          UploadTime = DateTime.UtcNow
        }
      },
    };
  }
  public static IEnumerable<ApiScope> GetApiScopes() {
    return new List<ApiScope> {
      new(name: "read",   displayName: "Read your data."),
      new(name: "write",  displayName: "Write your data."),
      new(name: "delete", displayName: "Delete your data."),
      new(name: "identityserverapi", displayName: "manage identityserver api endpoints."),
      new(name: "api1"),
      new(name: "offline_access")
    };
  }
}
