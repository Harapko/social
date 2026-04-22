using social.Models;

namespace social.Services.@interface;

public class UserService : IUserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Alice", Email = "alice@example.com" }
    };

    public List<User> GetAll() => _users;

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User Create(User user)
    {
        user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(user);
        return user;
    }

    public bool Update(int id, User updatedUser)
    {
        var user = GetById(id);
        if (user is null) return false;

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        return true;
    }

    public bool Delete(int id)
    {
        var user = GetById(id);
        if (user is null) return false;

        _users.Remove(user);
        return true;
    }
}