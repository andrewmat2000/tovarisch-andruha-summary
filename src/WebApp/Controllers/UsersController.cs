using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using TovarischAndruha.Summary.WebApp.Dtos;

namespace TovarischAndruha.Summary.WebApp.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase {
  [HttpPost("sign_in")]
  public async Task<ActionResult> SignInAsync([FromBody] SignInDto signInDto) {
    using var httpClient = new HttpClient();

    var content = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("client_id", "webapp"),
      new KeyValuePair<string, string>("username", signInDto.Login),
      new KeyValuePair<string, string>("grant_type", "password"),
      new KeyValuePair<string, string>("password", signInDto.Password),
    ]);

    var response = await httpClient.PostAsync(string.Format("{0}/connect/token", AppSettings.IdentityServerUrl), content);

    if (response.StatusCode != System.Net.HttpStatusCode.OK) {
      return NotFound();
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

    return Ok();
  }
  [HttpGet("sign_out")]
  public ActionResult SignOutAsync() {
    Response.Cookies.Delete(AppSettings.AuthorizationTokenCookieName);
    Response.Cookies.Delete(AppSettings.RefreshTokenCookieName);

    return Redirect("/");
  }
}