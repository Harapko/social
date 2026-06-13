namespace Social.Application.DTOs;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}

