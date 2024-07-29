using Microsoft.AspNetCore.Identity;

namespace TovarischAndruha.Summary.WebAppComponents.Identity;

public record CreateUserResponse(
  bool Success,
  IEnumerable<IdentityError>? Errors = null
);