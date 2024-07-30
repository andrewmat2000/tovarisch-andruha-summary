using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TovarischAndruha.Summary.WebApp.Dtos;
using TovarischAndruha.Summary.WebAppComponents.ViewModels;

namespace TovarischAndruha.Summary.WebApp.Controllers;

[Route("api/[controller]")]
public class UsersController(ILogger<UsersController> logger) : ControllerBase {
  private readonly ILogger<UsersController> _logger = logger;

  [HttpGet("get_profile")]
  public async Task<UserViewModel> GetProfile() {
    using var httpClient = new HttpClient();

    Claim? claim = User.Claims.FirstOrDefault(x => x.Properties.Any(p => p.Value == "sub"));

    if (claim == null) {
      return new() {
        DisplayName = "guest",
        Email = "guest@welcome.ru",
        Login = "guest"
      };
    }

    var response = await httpClient.GetAsync(string.Format("{0}/get_user?id={1}", AppSettings.IdentityServerUrl, claim.Value));

    if (response.StatusCode != System.Net.HttpStatusCode.OK) {
      throw new();
    }

    if (await response.Content.ReadFromJsonAsync<UserViewModel>() is not UserViewModel userViewModel) {
      throw new();
    }

    return userViewModel;
  }

  [HttpPost("sign_in")]
  [Consumes("application/x-www-form-urlencoded")]
  public async Task<ActionResult> SignInAsync([FromForm] SignInDto signInDto, [FromQuery] string? returnUrl = null) {
    using var httpClient = new HttpClient();

    var content = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("client_id", "webapp"),
      new KeyValuePair<string, string>("username", signInDto.Login),
      new KeyValuePair<string, string>("grant_type", "password"),
      new KeyValuePair<string, string>("password", signInDto.Password),
    ]);

    var response = await httpClient.PostAsync(string.Format("{0}/connect/token", AppSettings.IdentityServerUrl), content);

    if (response.StatusCode != System.Net.HttpStatusCode.OK) {
      return Redirect(string.Format("/users/sign_in?returnUrl={0}", returnUrl ?? "/"));
    }

    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>() ?? throw new NullReferenceException();

    HttpContext.Response.Cookies.Append(AppSettings.AuthorizationTokenCookieName, tokenResponse.AccessToken, new() {
      Secure = true,
      HttpOnly = true,
      Expires = DateTime.Now + TimeSpan.FromSeconds(tokenResponse.ExpiresIn),
      SameSite = SameSiteMode.Strict
    });

    HttpContext.Response.Cookies.Append(AppSettings.RefreshTokenCookieName, tokenResponse.RefreshToken, new() {
      Secure = true,
      HttpOnly = true,
      Expires = DateTime.Now + TimeSpan.FromDays(90),
      SameSite = SameSiteMode.Strict
    });

    return Redirect(returnUrl ?? "/");
  }

  [Authorize]
  [HttpGet("sign_out")]
  public ActionResult SignOutAsync() {
    Response.Cookies.Delete(AppSettings.AuthorizationTokenCookieName);
    Response.Cookies.Delete(AppSettings.RefreshTokenCookieName);

    return Redirect("/");
  }
}