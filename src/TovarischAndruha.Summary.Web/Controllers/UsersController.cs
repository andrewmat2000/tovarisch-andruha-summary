using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TovarischAndruha.Summary.Shared.Models;

namespace TovarischAndruha.Summary.Web;

[Route("users")]
public class UsersController : Controller {
  private readonly string _authServerUrl = Environment.GetEnvironmentVariable("AUTH_SERVER_URL");

  [HttpGet("sign_in")]
  public IActionResult SignIn() {
    return View();
  }
  [HttpPost("sign_in")]
  public async Task<IActionResult> SignIn(LoginRequest loginRequest) {
    using var httpClient = new HttpClient();

    var response = await httpClient.PostAsync(string.Format("{0}/Accounts/Login?returnUrl=/", _authServerUrl), JsonContent.Create(loginRequest));

    foreach (var header in response.Headers) {
      if (Response.Headers.ContainsKey(header.Key)) {
        Response.Headers.Remove(header.Key);
      }
      Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
    }

    return Ok();
  }
  [HttpGet("sign_up")]
  public IActionResult SignUp() {
    return View();
  }
  [HttpPost("sign_up")]
  public async Task<IActionResult> SignUp(CreateUserRequest createUserRequest) {
    using var httpClient = new HttpClient();

    var response = await httpClient.PostAsync(string.Format("{0}/Accounts/Register?returnUrl=/", _authServerUrl), JsonContent.Create(createUserRequest));

    var json = await response.Content.ReadFromJsonAsync<CreateUserResponse>();

    return Ok();
  }
}