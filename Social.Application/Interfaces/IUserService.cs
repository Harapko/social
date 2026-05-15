using Social.Application.DTOs;

namespace Social.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}