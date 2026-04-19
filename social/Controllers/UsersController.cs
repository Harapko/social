using Microsoft.AspNetCore.Mvc;
using social.Models;

namespace social.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> users = new List<User>()
    {
        new User { Id = 1, Name = "Alice", Email = "alice@example.com" }
    };

    [HttpGet]
    public IEnumerable<User> Get() => users;

    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user is null) return NotFound();

        return user;
    }

    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user is null) return NotFound();

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user is null) return NotFound();

        users.Remove(user);
        return NoContent();
    }
}