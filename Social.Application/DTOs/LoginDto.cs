using System.ComponentModel.DataAnnotations;

namespace Social.Application.DTOs;

public sealed record LoginDto
{
    [Required]
    public string EmailOrUsername { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}