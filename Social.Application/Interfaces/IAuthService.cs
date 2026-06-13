using Social.Application.DTOs;

namespace Social.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken);

    Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
}