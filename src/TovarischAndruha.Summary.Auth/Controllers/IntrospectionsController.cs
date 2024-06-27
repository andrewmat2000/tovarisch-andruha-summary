using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TovarischAndruha.Summary.Auth.OauthRequest;
using TovarischAndruha.Summary.Auth.Services;

namespace TovarischAndruha.Summary.Auth.Controllers {
  //[Route("api/[controller]")]
  // [ApiController]
  public class IntrospectionsController : ControllerBase {
    private readonly ITokenIntrospectionService _tokenIntrospectionService;
    public IntrospectionsController(ITokenIntrospectionService tokenIntrospectionService) {
      _tokenIntrospectionService = tokenIntrospectionService;
    }

    // [HttpPost("TokenIntrospect")]
    [HttpPost]
    public async Task<IActionResult> TokenIntrospect(TokenIntrospectionRequest tokenIntrospectionRequest) {
      var result = await _tokenIntrospectionService.IntrospectTokenAsync(tokenIntrospectionRequest);
      return Ok(result);
    }
  }
}
