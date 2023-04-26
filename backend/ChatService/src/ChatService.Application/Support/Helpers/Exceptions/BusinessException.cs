using System.Net;

namespace ChatService.Application.Support.Helpers.Exceptions;

public class BusinessException : ApplicationException
{
    public int StatusCode { get;}

    public BusinessException()
        => StatusCode = (int) HttpStatusCode.Conflict;
    
    public BusinessException(string customMessage): base(customMessage) => StatusCode = (int) HttpStatusCode.Conflict;
}