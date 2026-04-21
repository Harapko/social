using Social.Domain.Entities;

namespace Social.Application.Interfaces;

public interface IUserService
{
    List<User> GetAll();
    User? GetById(int id);
    User Create(User user);
    bool Update(int id, User user);
    bool Delete(int id);
}