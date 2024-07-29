using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TovarischAndruha.Summary.Identity.Domain.Attributes;

namespace TovarischAndruha.Summary.Identity.Domain.Models;

public class EditUserRequest : WebAppComponents.Identity.EditUserRequest {
  [Required]
  public new string Login { get; set; } = null!;
  [EmailAddress]
  public new string? Email { get; set; }
  [FullNameValidation]
  public new string? DisplayName { get; set; }
  public new IFormFile? Avatar { get; set; }
}