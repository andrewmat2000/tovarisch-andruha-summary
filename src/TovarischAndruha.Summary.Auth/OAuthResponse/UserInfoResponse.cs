using System.Text.Json.Serialization;

namespace TovarischAndruha.Summary.Auth.OAuthResponse {
  public class UserInfoResponse {
    public bool Succeeded { get; set; }
    public string Error { get; set; } = string.Empty;

    public string ErrorDescription { get; set; }
    public bool HasError => !string.IsNullOrEmpty(Error);


    /// <summary>
    /// Returned result as json.
    /// </summary>
    public string Claims { get; set; }

    [JsonPropertyName("sub")]
    public string Sub { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("email_verified")]
    public bool EmailVerified { get; set; }
  }
}
