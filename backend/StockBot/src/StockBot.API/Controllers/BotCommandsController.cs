using Microsoft.AspNetCore.Mvc;
using StockBot.Application.Services.Interfaces;

namespace StockBot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BotCommandsController : ControllerBase
{
    private readonly IBotCommandService _botCommandService;

    public BotCommandsController(IBotCommandService botCommandService)
    {
        _botCommandService = botCommandService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] string command)
    {
        _botCommandService.ExecuteStockCommandAsync(command);
        
        return Accepted();
    }
}