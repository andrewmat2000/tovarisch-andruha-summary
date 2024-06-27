/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using System.Collections.Generic;
using TovarischAndruha.Summary.Auth.Common;

namespace TovarischAndruha.Summary.Auth.Models {
  public class GrantTypes {
    public static IList<string> Code =>
        new[] { AuthorizationGrantTypesEnum.Code.GetEnumDescription() };

    public static IList<string> ClientCredentials =>
        new[] { AuthorizationGrantTypesEnum.ClientCredentials.GetEnumDescription() };

    public static IList<string> RefreshToken =>
        new[] { AuthorizationGrantTypesEnum.RefreshToken.GetEnumDescription() };

    public static IList<string> CodeAndClientCredentials =>
        new[] { AuthorizationGrantTypesEnum.ClientCredentials.GetEnumDescription(),
                AuthorizationGrantTypesEnum.Code.GetEnumDescription() };
    public static IList<string> DeviceCode =>
        new[] { AuthorizationGrantTypesEnum.DeviceCode.GetEnumDescription() };
  }
}
