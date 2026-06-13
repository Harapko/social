namespace Social.Application.DTOs;

public sealed record AuthResponseDto
{
    public string AccessToken { get; init; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; init; }
}