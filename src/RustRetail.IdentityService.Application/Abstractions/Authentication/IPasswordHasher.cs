﻿namespace RustRetail.IdentityService.Application.Abstractions.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
