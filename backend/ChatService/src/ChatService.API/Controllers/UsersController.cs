using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.API.Controllers;

[ApiController]
[AllowAnonymous]
[Produces("application/json")]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersServices _usersServices;

    public UsersController(IUsersServices usersServices)
    {
        _usersServices = usersServices;
    }
    
    /// <summary>
    /// Create new user for login
    /// </summary>
    /// <remarks>
    /// Create new user
    /// </remarks>
    /// <returns></returns>
    /// <param name="userDto">User Dto</param>
    /// <response code="201">Return created user</response>
    /// <response code="409">Business logic exception</response>
    /// /// /// <response code="500">Internal error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] UserDto userDto)
    {
        var newUser = await _usersServices.AddUserAsync(userDto);
        
        return StatusCode(StatusCodes.Status201Created, newUser);
    }
}