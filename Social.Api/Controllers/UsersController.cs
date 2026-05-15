using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Social.Api.DTOs;
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
    public async Task<ActionResult<UserDto>> Create(CreateUserRequestDto request, CancellationToken cancellationToken)
    {
        UserDto createdUser;

        try
        {
            createdUser = await _userService.CreateAsync(new CreateUserDto
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password
            }, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            return Conflict(new { message = "User with this email already exists." });
        }

        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequestDto updateUserRequestDto, CancellationToken cancellationToken)
    {
        bool isUpdated;

        try
        {
            isUpdated = await _userService.UpdateAsync(id, new UpdateUserDto
            {
                Email = updateUserRequestDto.Email,
                Name = updateUserRequestDto.Name,
                Surname = updateUserRequestDto.Surname,
                PhoneNumber = updateUserRequestDto.PhoneNumber,
                Password = updateUserRequestDto.Password
            }, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            return Conflict(new { message = "User with this email already exists." });
        }

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