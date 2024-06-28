namespace TovarischAndruha.Summary.Shared.Models;

public class CreateUserRequest {
  public string UserName { get; set; }
  public string Password { get; set; }
  public string RepeatPassword { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
}
