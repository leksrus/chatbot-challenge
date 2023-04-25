using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.API.Controllers;


[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;

    public MessagesController(IMessagesService messagesService)
    {
        _messagesService = messagesService;
    }
    
    
    /// <summary>
    /// Messages from chatroom
    /// </summary>
    /// <remarks>
    /// Get Last Messages from chatroom
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Return las list of messages</response>
    /// /// <response code="409">Business logic exception</response>
    /// <response code="500">Internal error</response>
    [HttpGet("{chatRoom}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MessageDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(string chatRoom)
    {
        var messages = _messagesService.GetLastMessagesForChannelAsync(chatRoom);
        
        return Ok(messages);
    }
    
    /// <summary>
    /// Create new message for history
    /// </summary>
    /// <remarks>
    /// Create new message
    /// </remarks>
    /// <returns></returns>
    /// <param name="messageDto">Message Dto</param>
    /// <response code="201">Return created message</response>
    /// /// /// <response code="500">Internal error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] MessageDto messageDto)
    {
        var newMessage = await _messagesService.AddMessageAsync(messageDto);
        
        return StatusCode(StatusCodes.Status201Created, newMessage);
    }
}