/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TovarischAndruha.Summary.Auth.Configuration;
using TovarischAndruha.Summary.Auth.Models.Context;
using TovarischAndruha.Summary.Auth.Models.Entities;
using TovarischAndruha.Summary.Auth.OAuthResponse;
using TovarischAndruha.Summary.Auth.Validations;

namespace TovarischAndruha.Summary.Auth.Services;

public class DeviceAuthorizationService : IDeviceAuthorizationService {
  private readonly IDeviceAuthorizationValidation _validation;
  private readonly OAuthServerOptions _options;
  private readonly BaseDBContext _dbContext;
  public DeviceAuthorizationService(
      IDeviceAuthorizationValidation validation,
      IOptions<OAuthServerOptions> options,
      BaseDBContext dBContext
      ) {
    _validation = validation;
    _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    _dbContext = dBContext;
  }

  public async Task<bool> DeviceFlowUserInteractionAsync(string userCode) {
    if (string.IsNullOrWhiteSpace(userCode)) {
      return false;
    }

    var data = await _dbContext.DeviceFlows.FindAsync(userCode);
    if (data != null) {
      if (data.ExpireIn > DateTime.Now) {
        data.UserInterActionComplete = true;
        _dbContext.Update(data);
        var result = await _dbContext.SaveChangesAsync();
        if (result > 0) {
          return true;
        }
        return false;
      }
      return false;
    } else {
      return false;
    }
  }

  public async Task<DeviceAuthorizationResponse> GenerateDeviceAuthorizationCodeAsync(HttpContext httpContext) {
    var validationResult = await _validation.ValidateAsync(httpContext);

    if (!validationResult.Succeeded) {
      return null;
    }

    var response = new DeviceAuthorizationResponse {
      UserCode = GenerateUserCode(),
      DeviceCode = GenerateDeviceCode(),
      VerificationUri = _options.IDPUri + "/device",
      ExpiresIn = 300, // user code and device code are valid for 5 minutes.
      Interval = _options.DeviceFlowInterval,

    };

    // Store the responst in the back store (sql server in my case)
    var deviceflowEntity = new DeviceFlowEntity {
      ClientId = validationResult.Client.ClientId,
      CreatedDate = DateTime.Now,
      UserCode = response.UserCode,
      DeviceCode = response.DeviceCode,
      ExpireIn = DateTime.Now.AddSeconds(response.ExpiresIn),
      UserInterActionComplete = false,
      SessionId = httpContext.Session.Id,
      RequestedScope = validationResult.RequestedScope != null ? validationResult.RequestedScope : default,

    };

    _dbContext.Add(deviceflowEntity);
    await _dbContext.SaveChangesAsync();
    return response;
  }


  // The main answer by Dan Rigby at https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
  // But I enhance the initiated of the rendom class to create a new thread for every request.
  private string GenerateUserCode(int? length = null) {
    length ??= 8;
    // Remove small letters and (Zero / One ) and I and O
    var chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
    var lengthCount = new char[length.Value];
    var random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

    for (int i = 0; i < lengthCount.Length; i++) {
      lengthCount[i] = chars[random.Value.Next(chars.Length)];
    }

    var result = new string(lengthCount);
    return result;
  }

  private static string GenerateDeviceCode(int? length = null) {
    length ??= 40;
    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var lengthCount = new char[length.Value];
    var random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

    for (int i = 0; i < lengthCount.Length; i++) {
      lengthCount[i] = chars[random.Value.Next(chars.Length)];
    }

    var result = new string(lengthCount);
    return result;
  }
}
