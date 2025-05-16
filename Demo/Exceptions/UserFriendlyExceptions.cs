using Microsoft.AspNetCore.Http;

namespace Demo.Exceptions;

public class UserFriendlyException : Exception
{
    
    public int StatusCode {get; set;} = StatusCodes.Status400BadRequest;

    public UserFriendlyException(string message) : base(message)
    {
    }
    
    public UserFriendlyException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}