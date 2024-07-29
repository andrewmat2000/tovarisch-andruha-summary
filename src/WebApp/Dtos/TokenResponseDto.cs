using System.Text.Json.Serialization;

namespace TovarischAndruha.Summary.WebApp.Dtos;

public class TokenResponseDto {
  [JsonPropertyName("access_token")]
  public string AccessToken { get; set; } = null!;
  [JsonPropertyName("refresh_token")]
  public string RefreshToken { get; set; } = null!;
  [JsonPropertyName("expires_in")]
  public int ExpiresIn { get; set; }
}