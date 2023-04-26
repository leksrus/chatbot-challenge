using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.API.Controllers;


[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>
    /// Obtain Token for Auth
    /// </summary>
    /// <remarks>
    /// Create Token
    /// </remarks>
    /// <returns></returns>
    /// <param name="userAuthDto">User Auth Dto</param>
    /// <response code="201">Return created auth token</response>
    /// <response code="401">Unauthorized exception</response>
    /// /// /// <response code="500">Internal error</response>
    [HttpPost("token")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] UserAuthDto userAuthDto)
    {
        var authDto = await _authService.GetTokenAsync(userAuthDto);
        
        return StatusCode(StatusCodes.Status201Created, authDto);
    }
}