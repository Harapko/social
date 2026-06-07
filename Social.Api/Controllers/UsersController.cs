using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Social.Application.DTOs;
using Social.Application.Interfaces;

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
    public async Task<ActionResult<List<UserDto>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto request, CancellationToken cancellationToken)
    {
        var createdUser = await _userService.CreateAsync(new CreateUserDto
        {
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password
        }, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto updateUserRequestDto, CancellationToken cancellationToken)
    {
        var isUpdated = await _userService.UpdateAsync(id, new UpdateUserDto
        {
            Email = updateUserRequestDto.Email,
            Name = updateUserRequestDto.Name,
            Surname = updateUserRequestDto.Surname,
            PhoneNumber = updateUserRequestDto.PhoneNumber,
            Password = updateUserRequestDto.Password
        }, cancellationToken);

        if (!isUpdated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _userService.DeleteAsync(id, cancellationToken);
        if (!isDeleted) return NotFound();

        return NoContent();
    }
}