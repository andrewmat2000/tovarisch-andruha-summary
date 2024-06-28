/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using Microsoft.AspNetCore.Mvc;
using TovarischAndruha.Summary.Auth.OauthRequest;
using TovarischAndruha.Summary.Auth.Services.Users;
using TovarischAndruha.Summary.Shared.Models;

namespace TovarischAndruha.Summary.Auth.Controllers;
public class AccountsController(IUserManagerService userManagerService) : Controller {
  private readonly IUserManagerService _userManagerService = userManagerService;

  [HttpPost]
  public async Task<IActionResult> Login([FromBody] LoginRequest request) {
    var result = await _userManagerService.LoginUserAsync(request);

    if (result.Succeeded) {
      return Ok();
    }

    return NotFound();
  }

  [HttpPost]
  public async Task<CreateUserResponse> Register([FromBody] CreateUserRequest request) {
    var result = await _userManagerService.CreateUserAsync(request);

    return result;
  }
}

