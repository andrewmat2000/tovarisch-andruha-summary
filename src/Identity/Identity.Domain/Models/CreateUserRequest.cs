using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TovarischAndruha.Summary.Identity.Domain.Attributes;

namespace TovarischAndruha.Summary.Identity.Domain.Models;

public class CreateUserRequest : WebAppComponents.Identity.CreateUserRequest {
  [Required]
  public new string Login { get; set; } = null!;
  [Required]
  public new string Password { get; set; } = null!;
  [Required]
  [EmailAddress]
  public new string Email { get; set; } = null!;
  [Required]
  [FullNameValidation]
  public new string DisplayName { get; set; } = null!;
  public new IFormFile? Avatar { get; set; }
}