using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TovarischAndruha.Summary.Auth.Validations.Response;

namespace TovarischAndruha.Summary.Auth.Validations {
  public interface IDeviceAuthorizationValidation {
    Task<DeviceAuthorizationValidationResponse> ValidateAsync(HttpContext httpContext);
  }
}
