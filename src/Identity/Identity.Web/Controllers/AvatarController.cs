using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Domain.Entities;

namespace UsersIdentity.Controllers;

[Route("[controller]")]
public class AvatarController : ControllerBase {
  private readonly UserManager<AppUser> _userManager;

  [HttpGet("get_avatar_by_login")]
  public async Task<ActionResult> GetAvatarByLogin([FromQuery] string login) {
    var user = await _userManager.Users.Include(x => x.Avatar).FirstOrDefaultAsync(x => x.UserName == login);

    if (user == null || user.Avatar == null) {
      return File(System.Array.Empty<byte>(), "image/png");
    }

    return File(user.Avatar.Photo, user.Avatar.Extension);
  }

  [HttpGet("get_avatar_by_id")]
  public async Task<ActionResult> GetAvatarById([FromQuery] string id) {
    var user = await _userManager.Users.Include(x => x.Avatar).FirstOrDefaultAsync(x => x.Id == id);

    if (user == null || user.Avatar == null) {
      return NotFound();
    }

    return File(user.Avatar.Photo, user.Avatar.Extension);
  }

  public AvatarController(UserManager<AppUser> userManager) {
    _userManager = userManager;
  }
}