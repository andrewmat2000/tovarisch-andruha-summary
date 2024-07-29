using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Domain.Entities;
using TovarischAndruha.Summary.Identity.Domain.Models;
using TovarischAndruha.Summary.Identity.Infrastructure;
using TovarischAndruha.Summary.WebAppComponents.ViewModels;
using CreateUserResponse = TovarischAndruha.Summary.WebAppComponents.Identity.CreateUserResponse;
using EditUserResponse = TovarischAndruha.Summary.WebAppComponents.Identity.EditUserResponse;

namespace UsersIdentity.Controllers;

[Route("/")]
public class HomeController : Controller {
  private readonly AppDbContext _appDbContext;
  private readonly UserManager<AppUser> _userManager;
  private readonly ILogger<HomeController> _logger;

  [HttpDelete("delete_user")]
  public async Task<ActionResult> DeleteUser([FromQuery] string login) {
    var user = await _userManager.FindByNameAsync(login);

    if (user == null) {
      return NotFound();
    }

    await _userManager.DeleteAsync(user);

    return Ok();
  }

  [HttpPost("create_user")]
  public async Task<CreateUserResponse> CreateUser([FromForm] CreateUserRequest request) {
    Avatar? avatar = null;

    if (!ModelState.IsValid) {
      var modelStateErrors = ModelState.Values.Where(x => x.ValidationState != ModelValidationState.Valid).ToArray();
      var errors = new IdentityError[ModelState.ErrorCount];

      for (var i = 0; i < errors.Length; i++) {
        var stringBuilder = new StringBuilder();
        foreach (var error in modelStateErrors[i].Errors) {
          stringBuilder.Append(error.ErrorMessage);
        }

        errors[i] = new IdentityError() {
          Code = "ValidationError",
          Description = stringBuilder.ToString()
        };
      }

      return new(false, errors);
    }

    if (Request.Form.Files.FirstOrDefault() is IFormFile formFile) {
      var buff = new byte[formFile.Length];

      using var stream = formFile.OpenReadStream();

      await stream.ReadAsync(buff);

      avatar = new Avatar() {
        Photo = buff,
        Extension = formFile.ContentType,
        UploadTime = DateTime.UtcNow
      };
    }

    if (avatar != null) {
      await _appDbContext.Avatars.AddAsync(avatar);
      await _appDbContext.SaveChangesAsync();
    }

    var result = await _userManager.CreateAsync(new() {
      DisplayName = request.DisplayName,
      UserName = request.Login,
      Email = request.Email,
      TwoFactorEnabled = false,
      EmailConfirmed = true,
      Avatar = avatar
    }, request.Password);

    return new(result.Succeeded, result.Errors);
  }

  [HttpPost("edit_user")]
  public async Task<EditUserResponse> EditUser([FromForm] EditUserRequest request) {
    var user = await _userManager.FindByNameAsync(request.Login);

    if (!ModelState.IsValid) {
      return new(false);
    }

    if (user == null) {
      return new(false);
    }

    Avatar? avatar = null;

    if (Request.Form.Files.FirstOrDefault() is IFormFile formFile) {
      var buff = new byte[formFile.Length];

      using var stream = formFile.OpenReadStream();

      await stream.ReadAsync(buff);

      avatar = new Avatar() {
        Photo = buff,
        Extension = formFile.ContentType,
        UploadTime = DateTime.UtcNow
      };
    }

    if (avatar != null) {
      if (await _appDbContext.Users.Include(x => x.Avatar).FirstOrDefaultAsync(x => x.UserName == request.Login) is AppUser appUser &&
          appUser.Avatar != null) {
        _appDbContext.Avatars.Remove(appUser.Avatar);
      }

      await _appDbContext.Avatars.AddAsync(avatar);
      await _appDbContext.SaveChangesAsync();
    }

    user.Avatar = avatar ?? user.Avatar;
    user.DisplayName = request.DisplayName ?? user.DisplayName;
    user.Email = request.Email ?? user.Email;

    _appDbContext.Users.Update(user);
    await _appDbContext.SaveChangesAsync();

    return new(true);
  }

  [HttpGet("check_login")]
  public async Task<ActionResult> CheckLogin([FromQuery] string login) {
    var user = await _userManager.FindByNameAsync(login);

    if (user == null) {
      return Ok();
    }

    return BadRequest();
  }

  [HttpGet("check_email")]
  public async Task<ActionResult> CheckEmail([FromQuery] string email) {
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null) {
      return Ok();
    }

    return BadRequest();
  }

  [HttpGet("get_users")]
  public async Task<IEnumerable<UserViewModel>> GetUsers() {
    var originalUsers = await _userManager.Users.Include(x => x.Avatar).ToArrayAsync();

    var users = new UserViewModel[originalUsers.Length];

    for (var i = 0; i < users.Length; i++) {
      users[i] = new() {
        Login = originalUsers[i].UserName,
        Email = originalUsers[i].Email,
        DisplayName = originalUsers[i].DisplayName,
      };
    }

    return users;
  }

  [HttpGet("get_user")]
  public async Task<UserViewModel> GetUser([FromQuery] string id) {
    var user = await _userManager.FindByIdAsync(id);

    if (user == null) {
      throw new NullReferenceException();
    }

    return new UserViewModel() {
      Email = user.Email,
      DisplayName = user.DisplayName,
      Login = user.UserName,
    };
  }

  public HomeController(AppDbContext appDbContext, UserManager<AppUser> userManager, ILogger<HomeController> logger) {
    _appDbContext = appDbContext;
    _userManager = userManager;
    _logger = logger;
  }
}