using TovarischAndruha.Summary.Auth.Enumeration;

namespace TovarischAndruha.Summary.Auth.Validations.Response {
  public class BearerTokenUsageTypeValidationResponse : BaseValidationResponse {
    public string Token { get; set; }
    public BearerTokenUsageTypeEnum BearerTokenUsageType { get; set; }
  }
}
