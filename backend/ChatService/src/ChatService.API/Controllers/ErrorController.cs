using ChatService.Application.Support.Helpers;
using ChatService.Application.Support.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.API.Controllers;

[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (context == null)
        {
            return NotFound();
        }

        var businessException = context.Error as BusinessException;
        
        var errorResponse = Problem(
            instance: context.Path,
            title: context.Error.Message,
            detail: context.Error.StackTrace,
            statusCode: businessException?.StatusCode
        );        
        

        return errorResponse;
    }
}