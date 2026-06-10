using Social.Application.DTOs;
using Social.Application.Interfaces;
using Social.Domain.Entities;

namespace Social.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _repo.GetAllAsync(cancellationToken);
        return users.Select(ToDto).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repo.GetByIdAsync(id, cancellationToken);
        return user is null ? null : ToDto(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = ToEntity(dto);

        await _repo.AddAsync(user, cancellationToken);

        return ToDto(user);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        UpdateEntity(existing, dto);

        return await _repo.UpdateAsync(existing, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        return await _repo.DeleteAsync(existing, cancellationToken);
    }

    private static User ToEntity(CreateUserDto dto) => new()
    {
        Email = dto.Email.Trim(),
        Name = dto.Name.Trim(),
        Surname = dto.Surname.Trim(),
        PhoneNumber = dto.PhoneNumber.Trim(),
        PasswordHash = dto.Password
    };

    private static void UpdateEntity(User user, UpdateUserDto dto)
    {
        user.Email = dto.Email.Trim();
        user.Name = dto.Name.Trim();
        user.Surname = dto.Surname.Trim();
        user.PhoneNumber = dto.PhoneNumber.Trim();

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHash = dto.Password;
    }

    private static UserDto ToDto(User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        Surname = user.Surname,
        PhoneNumber = user.PhoneNumber,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt
    };
}