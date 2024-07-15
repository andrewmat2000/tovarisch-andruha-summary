using Microsoft.AspNetCore.Mvc;

namespace TovarischAndruha.Summary.Identity.Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase {
  public string Index() {
    return "Hello world!";
  }
}