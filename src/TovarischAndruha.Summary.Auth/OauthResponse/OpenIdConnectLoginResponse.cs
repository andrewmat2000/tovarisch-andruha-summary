/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System.Collections.Generic;
using TovarischAndruha.Summary.Auth.Models.Entities;

namespace TovarischAndruha.Summary.Auth.OauthResponse {
  public class OpenIdConnectLoginResponse {

    public string UserName { get; set; }
    public string Password { get; set; }
    public string RedirectUri { get; set; }
    public string Code { get; set; }
    public IList<string> RequestedScopes { get; set; }

    public AppUser AppUser { get; set; }
    public bool Succeeded { get; set; }
    public string Error { get; set; } = string.Empty;

    public string ErrorDescription { get; set; }
    public bool HasError => !string.IsNullOrEmpty(Error);

  }
}
