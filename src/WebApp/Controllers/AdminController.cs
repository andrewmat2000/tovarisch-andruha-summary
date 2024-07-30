using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TovarischAndruha.Summary.WebAppComponents.Identity;
using TovarischAndruha.Summary.WebAppComponents.ViewModels;

namespace TovarischAndruha.Summary.WebApp.Controllers;

[Authorize]
[Route("/api/[controller]")]
public class AdminController(ILogger<AdminController> logger) : ControllerBase {
  private readonly ILogger<AdminController> _logger = logger;

  private StreamContent CreateFileContent(Stream stream, string fileName, string contentType) {
    var fileContent = new StreamContent(stream);
    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") {
      Name = "\"files\"",
      FileName = "\"" + fileName + "\""
    };
    fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
    return fileContent;
  }

  [HttpDelete("delete_user")]
  public async Task<ActionResult> DeleteUser([FromQuery] string login) {
    using var httpClient = new HttpClient();
    var response = await httpClient.DeleteAsync(string.Format("{0}/delete_user?{1}={2}", AppSettings.IdentityServerUrl, nameof(login), login));

    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
      return Ok();
    }

    return NotFound();
  }

  [HttpPost("edit_user")]
  public async Task<EditUserResponse> EditUser([FromForm] EditUserRequest request) {
    using var httpClient = new HttpClient();
    var content = new MultipartFormDataContent {
      { new StringContent(request.Login), nameof(request.Login) }
    };

    if (request.DisplayName != null) {
      content.Add(new StringContent(request.DisplayName), nameof(request.DisplayName));
    }

    if (request.Email != null) {
      content.Add(new StringContent(request.Email), nameof(request.Email));
    }

    if (request.Avatar != null) {
      content.Add(CreateFileContent(request.Avatar.OpenReadStream(), request.Avatar.FileName, request.Avatar.ContentType));
    }

    var response = await httpClient.PostAsync(string.Format("{0}/edit_user", AppSettings.IdentityServerUrl), content);

    var editUserResponse = await response.Content.ReadFromJsonAsync<EditUserResponse>();

    if (editUserResponse == null) {
      throw new NullReferenceException();
    }

    return editUserResponse;
  }

  [HttpPost("create_user")]
  public async Task<CreateUserResponse> CreateUser([FromForm] CreateUserRequest request) {
    using var httpClient = new HttpClient();
    var content = new MultipartFormDataContent {
      { new StringContent(request.DisplayName), nameof(request.DisplayName) },
      { new StringContent(request.Login), nameof(request.Login) },
      { new StringContent(request.Password),nameof(request.Password) },
      { new StringContent(request.Email), nameof(request.Email) },
    };

    if (request.Avatar != null) {
      content.Add(CreateFileContent(request.Avatar.OpenReadStream(), request.Avatar.FileName, request.Avatar.ContentType));
    }

    var response = await httpClient.PostAsync(string.Format("{0}/create_user", AppSettings.IdentityServerUrl), content);

    var createUserResponse = await response.Content.ReadFromJsonAsync<CreateUserResponse>();

    if (createUserResponse == null) {
      throw new NullReferenceException();
    }

    return createUserResponse;
  }

  [HttpGet("check_login")]
  public async Task<ActionResult> CheckLogin([FromQuery] string login) {
    using var httpClient = new HttpClient();
    var response = await httpClient.GetAsync(string.Format("{0}/check_login?{1}={2}", AppSettings.IdentityServerUrl, nameof(login), login));

    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
      return Ok();
    }

    return BadRequest();
  }

  [HttpGet("check_email")]
  public async Task<ActionResult> CheckEmail([FromQuery] string email) {
    using var httpClient = new HttpClient();

    var response = await httpClient.GetAsync(string.Format("{0}/check_email?{1}={2}", AppSettings.IdentityServerUrl, nameof(email), email));

    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
      return Ok();
    }

    return BadRequest();
  }

  [HttpGet("get_users")]
  public async Task<IEnumerable<UserViewModel>> GetUsers() {
    using var httpClient = new HttpClient();
    return await httpClient.GetFromJsonAsync<UserViewModel[]>(string.Format("{0}/get_users", AppSettings.IdentityServerUrl)) ?? throw new NullReferenceException();
  }

  [HttpGet("get_avatar")]
  public async Task<ActionResult> GetAvatar([FromQuery] string login) {
    using var httpClient = new HttpClient();
    var response = await httpClient.GetAsync(string.Format("{0}/avatar/get_avatar_by_login?login={1}", AppSettings.IdentityServerUrl, login));

    if (response.Content.Headers.ContentType == null || response.Content.Headers.ContentType.MediaType == null) {
      throw new NullReferenceException();
    }

    return File(await response.Content.ReadAsByteArrayAsync(), response.Content.Headers.ContentType.MediaType);
  }
}