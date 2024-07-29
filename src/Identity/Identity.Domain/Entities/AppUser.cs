using Microsoft.AspNetCore.Identity;

namespace TovarischAndruha.Summary.Identity.Domain.Entities;
// Add profile data for application users by adding properties to the ApplicationUser class
public class AppUser : IdentityUser {
  public string DisplayName { get; set; } = string.Empty;
  public Avatar? Avatar { get; set; }
}