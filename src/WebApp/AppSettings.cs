namespace TovarischAndruha.Summary.WebApp;

public static class AppSettings {
  public static string IdentityServerUrl { get; } = Environment.GetEnvironmentVariable("IDENTITY_SERVER_URL") ?? "";
  public static string AuthorizationTokenCookieName { get; } = "Authorization";
  public static string RefreshTokenCookieName { get; } = "Refresh";
  public static string HostUrl { get; } = Environment.GetEnvironmentVariable("HOST_URL") ?? "http://localhost";
}