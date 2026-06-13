namespace Social.Application.DTOs;

public sealed record UpdateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string? Password { get; init; }
}
