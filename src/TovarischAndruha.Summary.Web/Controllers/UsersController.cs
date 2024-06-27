using Microsoft.AspNetCore.Mvc;

namespace TovarischAndruha.Summary.Web;

[Route("users")]
public class UsersController : Controller {
  [HttpGet("sign_in")]
  public IActionResult SignIn() {
    return View();
  }
}