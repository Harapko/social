using Microsoft.AspNetCore.Mvc;
using Social.Api.DTOs;
using Social.Application.Interfaces;
using Social.Domain.Entities;

namespace Social.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
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
    public IActionResult Create(CreateUserRequestDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone
        };

        var createdUser = _userService.Create(user);

        return Ok(createdUser);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CreateUserRequestDto updateUserRequestDto)
    {
        var user = new User
        {
            Name = updateUserRequestDto.Name,
            Email = updateUserRequestDto.Email
        };

        var isUpdated = _userService.Update(id, user);
        if (!isUpdated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _userService.Delete(id);
        if (!isDeleted) return NotFound();

        return NoContent();
    }
}