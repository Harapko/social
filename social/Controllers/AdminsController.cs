using Microsoft.AspNetCore.Mvc;
using social.Models;

namespace social.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminsController : ControllerBase
{
    private static readonly List<Admin> admins = new()
    {
        new Admin { Id = 1, Name = "Admin1", Email = "admin1@example.com" },
        new Admin { Id = 2, Name = "Admin2", Email = "admin2@example.com" }
    };

    [HttpGet]
    public IEnumerable<Admin> Get() => admins;

    [HttpGet("{id}")]
    public ActionResult<Admin> GetById(int id)
    {
        var admin = admins.FirstOrDefault(a => a.Id == id);
        if (admin is null) return NotFound();
        return admin;
    }

    [HttpPost]
    public ActionResult<Admin> Create(Admin admin)
    {
        admin.Id = admins.Any() ? admins.Max(a => a.Id) + 1 : 1;
        admins.Add(admin);

        return CreatedAtAction(nameof(GetById), new { id = admin.Id }, admin);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Admin updatedAdmin)
    {
        var admin = admins.FirstOrDefault(a => a.Id == id);
        if (admin is null) return NotFound();

        admin.Name = updatedAdmin.Name;
        admin.Email = updatedAdmin.Email;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var admin = admins.FirstOrDefault(a => a.Id == id);
        if (admin is null) return NotFound();

        admins.Remove(admin);
        return NoContent();
    }
}