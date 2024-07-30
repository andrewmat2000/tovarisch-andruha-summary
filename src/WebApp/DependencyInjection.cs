using Microsoft.AspNetCore.Authentication.JwtBearer;
using TovarischAndruha.Summary.WebApp;
using IdentityModel.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection {
  public static IServiceCollection AddMyAuthentication(this IServiceCollection services) {
    services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
        options.Authority = AppSettings.IdentityServerUrl;

        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new() {
          //ValidateIssuer = true,
          //ValidateAudience = true,
          //ValidateIssuerSigningKey = true,

          ValidateAudience = false,
          ValidateIssuer = false,
        };
      });
    return services;
  }

  public static WebApplication UseMyAuthorization(this WebApplication app) {
    app.Use(async (ctx, next) => {
      ctx.Request.Host = new(AppSettings.HostUrl);

      if (ctx.Request.Cookies.TryGetValue(AppSettings.AuthorizationTokenCookieName, out var value)) {
        ctx.Request.Headers.Authorization = string.Format("Bearer {0}", value);
      }

      await next();
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.Use(async (context, next) => {
      if (context.User.Identity?.IsAuthenticated == true) {
        await next();

        return;
      }

      if (context.Request.Cookies.TryGetValue(AppSettings.RefreshTokenCookieName, out var refreshToken) && refreshToken != null) {
        using var httpClient = new HttpClient();

        var response = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest {
          Address = string.Format("{0}/connect/token", AppSettings.IdentityServerUrl),
          GrantType = "refresh_token",
          ClientId = "webapp",
          RefreshToken = refreshToken
        });

        if (response == null || response.AccessToken == null || response.RefreshToken == null) {
          context.Response.Cookies.Delete(AppSettings.AuthorizationTokenCookieName);
          context.Response.Cookies.Delete(AppSettings.RefreshTokenCookieName);

          await next();

          return;
        }

        context.Response.Cookies.Append(AppSettings.AuthorizationTokenCookieName, response.AccessToken, new() {
          Secure = true,
          HttpOnly = true,
          Expires = DateTime.Now + TimeSpan.FromSeconds(response.ExpiresIn),
          SameSite = SameSiteMode.Strict
        });

        context.Response.Cookies.Append(AppSettings.RefreshTokenCookieName, response.RefreshToken, new() {
          Secure = true,
          HttpOnly = true,
          Expires = DateTime.Now + TimeSpan.FromDays(90),
          SameSite = SameSiteMode.Strict
        });

        context.Response.Redirect(context.Request.Path);

        return;
      }

      if (context.Request.Headers.TryGetValue("Accept", out var accept) &&
          !context.Request.Path.StartsWithSegments("/users/sign_in") &&
          !context.Request.Path.StartsWithSegments("/api/users/sign_in") &&
          !context.Request.Path.StartsWithSegments("/swagger") &&
          accept.ToString().StartsWith("text/html")) {

        context.Response.Redirect(string.Format("/users/sign_in?returnUrl={0}", context.Request.Path));

        return;
      }

      await next();
    });

    return app;
  }

  public static void UseSvelteSpaRouting(this WebApplication app, string[]? exclude = null) {
    app.Use(async (context, next) => {
      var url = context.Request.Path;

      if (context.Request.Headers.TryGetValue("Accept", out var accept) &&
          accept.ToString().StartsWith("text/html") &&
          (exclude == null || !exclude.Any(x => x.StartsWith(url)))) {
        context.Request.Path = new PathString(url + ".html");
      }

      await next();
    });
  }
}