using System.Net;

namespace ChatService.Application.Support.Helpers.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public int StatusCode { get;}

    public UnauthorizedException()
        => StatusCode = (int) HttpStatusCode.Unauthorized;
    
    public UnauthorizedException(string customMessage): base(customMessage) => StatusCode = (int) HttpStatusCode.Conflict;
}