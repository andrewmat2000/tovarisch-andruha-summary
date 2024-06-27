/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TovarischAndruha.Summary.Auth.Models.Entities;
using TovarischAndruha.Summary.Auth.OauthRequest;
using TovarischAndruha.Summary.Auth.OauthResponse;
using TovarischAndruha.Summary.Shared.Models;

namespace TovarischAndruha.Summary.Auth.Services.Users {
  public class UserManagerService : IUserManagerService {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<UserManagerService> _logger;

    public UserManagerService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ILogger<UserManagerService> logger) {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    public async Task<AppUser> GetUserAsync(string userId) {
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null) {
        return null;
      } else {
        return user;
      }
    }

    public async Task<LoginResponse> LoginUserAsync(LoginRequest request) {
      var validationResult = ValidateLoginRequest(request);

      if (!validationResult) {
        _logger.LogInformation("validation for login process is failed {request}", request);
        return new LoginResponse { Error = "login process is failed" };
      }

      AppUser user = await _userManager.FindByNameAsync(request.UserName);
      if (user == null && request.UserName.Contains("@")) {
        user = await _userManager.FindByEmailAsync(request.UserName);
      }

      if (user == null) {
        _logger.LogInformation("creditioanl {userName}", request.UserName);
        return new LoginResponse { Error = "No user has this creditioanl" };
      }

      await _signInManager.SignOutAsync();


      Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager
          .PasswordSignInAsync(user, request.Password, false, false);

      if (loginResult.Succeeded) {
        return new LoginResponse { Succeeded = true };
      }

      return new LoginResponse { Succeeded = false, Error = "Login is not Succeeded" };

    }

    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request) {
      var validationResult = ValidateCreateUserRequest(request);

      if (!validationResult) {
        _logger.LogInformation("The create user request is failed please check your input {request}", request);
        return new CreateUserResponse { Error = "The create user request is failed please check your input" };
      }


      var user = new AppUser {
        UserName = request.UserName,
        Email = request.Email,
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        PhoneNumber = request.PhoneNumber,
        TwoFactorEnabled = false,
      };
      var createUserResult = await _userManager.CreateAsync(user, request.Password);

      if (createUserResult.Succeeded) {
        return new CreateUserResponse { Succeeded = true };
      }

      return new CreateUserResponse { Error = createUserResult.Errors.Select(x => x.Description).FirstOrDefault() };
    }


    public async Task<OpenIdConnectLoginResponse> LoginUserByOpenIdAsync(OpenIdConnectLoginRequest request) {
      bool validationResult = ValidateOpenIdLoginRequest(request);
      if (!validationResult) {
        _logger.LogInformation("login process is failed for request: {request}", request);
        return new OpenIdConnectLoginResponse { Error = "The login process is failed" };
      }

      AppUser user = await _userManager.FindByNameAsync(request.UserName);
      if (user == null && request.UserName.Contains("@")) {
        user = await _userManager.FindByEmailAsync(request.UserName);
      }

      if (user == null) {
        _logger.LogInformation("creditioanl {userName}", request.UserName);
        return new OpenIdConnectLoginResponse { Error = "No user has this creditioanl" };
      }

      await _signInManager.SignOutAsync();


      Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager
          .PasswordSignInAsync(user, request.Password, false, false);

      if (loginResult.Succeeded) {
        return new OpenIdConnectLoginResponse { Succeeded = true, AppUser = user };
      }

      return new OpenIdConnectLoginResponse { Succeeded = false, Error = "Login is not Succeeded" };
    }

    #region Helper Functions
    private bool ValidateLoginRequest(LoginRequest request) {
      if (request.UserName == null || request.Password == null) {
        return false;
      }

      if (request.Password.Length < 8) {
        return false;
      }

      return true;
    }

    private bool ValidateOpenIdLoginRequest(OpenIdConnectLoginRequest request) {
      if (request.Code == null || request.UserName == null || request.Password == null) {
        return false;
      }

      return true;
    }

    private bool ValidateCreateUserRequest(CreateUserRequest request) {
      if (request.UserName == null || request.Password == null || request.Email == null) {
        return false;
      }

      return true;
    }

    #endregion
  }
}
