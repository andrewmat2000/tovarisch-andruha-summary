using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TovarischAndruha.Summary.Auth.OAuthRequest;
using TovarischAndruha.Summary.Auth.Services;

namespace TovarischAndruha.Summary.Auth.Controllers {
  //[Route("api/[controller]")]
  //[ApiController]
  public class DeviceAuthorizationEndpointController : Controller {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDeviceAuthorizationService _deviceAuthorizationService;
    public DeviceAuthorizationEndpointController(IHttpContextAccessor httpContextAccessor,
        IDeviceAuthorizationService deviceAuthorizationService) {
      _httpContextAccessor = httpContextAccessor;
      _deviceAuthorizationService = deviceAuthorizationService;
    }

    [HttpPost("~/DeviceAuthorization")]
    public async Task<JsonResult> DeviceAuthorization() {
      var result = await _deviceAuthorizationService
          .GenerateDeviceAuthorizationCodeAsync(_httpContextAccessor.HttpContext);
      if (result != null) {
        return Json(result);
      }

      return Json("invalid client");
    }

    [HttpGet("~/device")]
    public IActionResult Device() {
      return View();
    }

    [HttpPost("~/device")]
    public async Task<IActionResult> Device(UserInteractionRequest userInteractionRequest) {
      var result = await _deviceAuthorizationService.DeviceFlowUserInteractionAsync(userInteractionRequest.UserCode);
      if (result == true) {
        return RedirectToAction("Index", "Home");
      } else {
        return View(userInteractionRequest);
      }
    }
  }
}
