using System.ComponentModel.DataAnnotations;

namespace Social.Application.DTOs;

public sealed record RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Username { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Surname { get; init; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string PhoneNumber { get; init; } = string.Empty;

    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain uppercase letter, lowercase letter and digit.")]
    public string Password { get; init; } = string.Empty;
}