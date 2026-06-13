using Social.Domain.Entities;

namespace Social.Application.Interfaces;

public interface IPasswordHasherService
{
    string HashPassword(User user, string password);

    bool VerifyPassword(User user, string password);
}