/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TovarischAndruha.Summary.Auth.Validations.Response;

namespace TovarischAndruha.Summary.Auth.Validations {
  public interface ITokenRevocationValidation {
    Task<TokenRevocationValidationResponse> ValidateAsync(HttpContext context);
  }
}
