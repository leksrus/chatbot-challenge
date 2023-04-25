using Microsoft.AspNetCore.Mvc;
using StockBot.Application.DTOs;
using StockBot.Application.Services.Interfaces;

namespace StockBot.API.Controllers;

[ApiController]
[Consumes("application/json")]
[Route("api/[controller]")]
public class BotCommandsController : ControllerBase
{
    private readonly IBotCommandService _botCommandService;

    public BotCommandsController(IBotCommandService botCommandService)
    {
        _botCommandService = botCommandService;
    }

    /// <summary>
    /// Send command to bot for execute
    /// </summary>
    /// <remarks>
    /// Send command to bot
    /// </remarks>
    /// <returns></returns>
    /// <param name="chatRequestDto">Command and Channel</param>
    /// <response code="202">Return accepted command</response>
    /// <response code="409">Business logic exception</response>
    /// /// /// <response code="500">Internal error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] ChatRequestDto chatRequestDto)
    {
        await _botCommandService.ExecuteStockCommandAsync(chatRequestDto);
        
        return Accepted();
    }
}