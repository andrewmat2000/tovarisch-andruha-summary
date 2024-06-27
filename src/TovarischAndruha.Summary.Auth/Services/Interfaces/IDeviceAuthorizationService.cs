using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TovarischAndruha.Summary.Auth.OAuthResponse;

namespace TovarischAndruha.Summary.Auth.Services {
  public interface IDeviceAuthorizationService {
    Task<DeviceAuthorizationResponse> GenerateDeviceAuthorizationCodeAsync(HttpContext httpContext);
    Task<bool> DeviceFlowUserInteractionAsync(string userCode);
  }
}
