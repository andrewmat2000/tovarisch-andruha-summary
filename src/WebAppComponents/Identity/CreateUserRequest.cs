using Microsoft.AspNetCore.Http;

namespace TovarischAndruha.Summary.WebAppComponents.Identity;

public class CreateUserRequest {
  public string Login { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string DisplayName { get; set; } = null!;
  public IFormFile? Avatar { get; set; }
}