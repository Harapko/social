using Microsoft.AspNetCore.Mvc;
using social.Models;

namespace social.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = users.FirstOrDefault(user => user.Id == id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            user.Id = users.Max(user => user.Id) + 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(user => user.Id == id);
            if (user == null) return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(user => user.Id == id);
            if (user == null) return NotFound();

            users.Remove(user);
            return NoContent();
        }
    }
}