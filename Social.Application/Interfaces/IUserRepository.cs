using Social.Domain.Entities;

namespace Social.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(User user, CancellationToken cancellationToken);
}

