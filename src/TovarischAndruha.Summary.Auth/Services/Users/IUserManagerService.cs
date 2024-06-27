/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System.Threading.Tasks;
using TovarischAndruha.Summary.Auth.Models.Entities;
using TovarischAndruha.Summary.Auth.OauthRequest;
using TovarischAndruha.Summary.Auth.OauthResponse;
using TovarischAndruha.Summary.Shared.Models;

namespace TovarischAndruha.Summary.Auth.Services.Users {
  public interface IUserManagerService {
    Task<AppUser> GetUserAsync(string userId);
    Task<LoginResponse> LoginUserAsync(LoginRequest request);
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
    Task<OpenIdConnectLoginResponse> LoginUserByOpenIdAsync(OpenIdConnectLoginRequest request);
  }
}
