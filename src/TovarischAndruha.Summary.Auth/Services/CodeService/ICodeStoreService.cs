/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System.Collections.Generic;
using System.Security.Claims;
using TovarischAndruha.Summary.Auth.Models;

namespace TovarischAndruha.Summary.Auth.Services.CodeService {
  public interface ICodeStoreService {
    string GenerateAuthorizationCode(AuthorizationCode authorizationCode);
    AuthorizationCode GetClientDataByCode(string key);
    AuthorizationCode UpdatedClientDataByCode(string key, ClaimsPrincipal claimsPrincipal, IList<string> requestdScopes);
    AuthorizationCode RemoveClientDataByCode(string key);
  }
}
