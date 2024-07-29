using System.Security.Claims;
using TovarischAndruha.Summary.Identity.Domain.Entities;

namespace TovarischAndruha.Summary.Identity.Domain;

public class TestAppUser : AppUser {
  public string Password { get; set; } = null!;
  public Claim[] Claims { get; set; } = null!;
}