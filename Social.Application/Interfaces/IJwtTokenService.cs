using Social.Domain.Entities;

namespace Social.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}