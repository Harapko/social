using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces;
using Social.Domain.Entities;
using Social.Infrastructure.Data;

namespace Social.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly SocialDbContext _context;

    public UserRepository(SocialDbContext context)
    {
        _context = context;
    }

    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        => _context.Users.AsNoTracking().ToListAsync(cancellationToken);

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        => _context.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);

    public Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername, CancellationToken cancellationToken)
        => _context.Users.FirstOrDefaultAsync(
            x => x.Email == emailOrUsername || x.Username == emailOrUsername,
            cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);

        var saved = await _context.SaveChangesAsync(cancellationToken);

        return saved > 0;
    }

    public async Task<bool> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Remove(user);

        var saved = await _context.SaveChangesAsync(cancellationToken);

        return saved > 0;
    }
}