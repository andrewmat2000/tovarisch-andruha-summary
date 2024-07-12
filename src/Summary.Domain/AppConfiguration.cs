namespace Summary.Domain;

public class AppConfiguration {
  public string DbConnectionString { get; set; } = Environment.GetEnvironmentVariable("PG_CONNECTION_STRING") ?? "";
}