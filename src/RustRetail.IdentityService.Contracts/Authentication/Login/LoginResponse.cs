﻿namespace RustRetail.IdentityService.Contracts.Authentication.Login
{
    public record LoginResponse(
        string AccessToken,
        string RefreshToken);
}
