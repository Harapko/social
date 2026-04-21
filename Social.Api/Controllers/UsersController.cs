using Microsoft.AspNetCore.Mvc;
using Social.Application.Interfaces;
using Social.Domain.Entities;
using Social.Api.DTOs;

namespace Social.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email
        };

        var created = _userService.Create(user);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email
        };

        var result = _userService.Update(id, user);
        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _userService.Delete(id);
        if (!result) return NotFound();

        return NoContent();
    }
}