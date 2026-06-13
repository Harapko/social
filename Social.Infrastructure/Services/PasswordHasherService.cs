using Microsoft.AspNetCore.Identity;
using Social.Application.Interfaces;
using Social.Domain.Entities;

namespace Social.Infrastructure.Services;

public sealed class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user , user.PasswordHash , password);

        return result == PasswordVerificationResult.Success;
    }
}