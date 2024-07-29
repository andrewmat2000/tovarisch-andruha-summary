namespace TovarischAndruha.Summary.Identity.Domain;

public static class AppSettings {
  public static string HostUrl { get; } = Environment.GetEnvironmentVariable("HOST_URL") ?? "localhost";
  public static string DbConnectionString { get; } = Environment.GetEnvironmentVariable("PG_CONNECTION_STRING") ?? "";
  public static string DefaultUserAvatarPath { get; } = "./default-admin-avatar.jpg";
  public static string DefaultUserPassword { get; } = Environment.GetEnvironmentVariable("DEFAULT_USER_PASSWORD") ?? "qwerty";
}