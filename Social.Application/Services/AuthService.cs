using Social.Application.DTOs;
using Social.Application.Interfaces;
using Social.Domain.Entities;

namespace Social.Application.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasherService _passwordHasherService;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken)
    {
        var email = dto.Email.Trim();
        var username = dto.Username.Trim();

        var existingEmailUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (existingEmailUser is not null)
            throw new InvalidOperationException("Email is already taken.");

        var existingUsernameUser = await _userRepository.GetByUsernameAsync(username, cancellationToken);
        if (existingUsernameUser is not null)
            throw new InvalidOperationException("Username is already taken.");

        var user = new User
        {
            Email = email,
            Username = username,
            Name = dto.Name.Trim(),
            Surname = dto.Surname.Trim(),
            PhoneNumber = dto.PhoneNumber.Trim()
        };

        user.PasswordHash = _passwordHasherService.HashPassword(user, dto.Password);

        await _userRepository.AddAsync(user, cancellationToken);

        var accessToken = _jwtTokenService.GenerateAccessToken(user);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var emailOrUsername = dto.EmailOrUsername.Trim();

        var user = await _userRepository.GetByEmailOrUsernameAsync(emailOrUsername, cancellationToken);
        if (user is null)
            throw new InvalidOperationException("Invalid email/username or password.");

        var isPasswordValid = _passwordHasherService.VerifyPassword(user, dto.Password);
        if (!isPasswordValid)
            throw new InvalidOperationException("Invalid email/username or password.");

        var accessToken = _jwtTokenService.GenerateAccessToken(user);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30)
        };
    }
}