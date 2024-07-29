using Microsoft.AspNetCore.Http;

namespace TovarischAndruha.Summary.WebAppComponents.Identity;

public class EditUserRequest {
  public string Login { get; set; } = null!;
  public string? Email { get; set; }
  public string? DisplayName { get; set; }
  public IFormFile? Avatar { get; set; }
}