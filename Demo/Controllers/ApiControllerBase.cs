using Demo.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleException(Exception ex)
    {
        if (ex is UserFriendlyException userFriendlyException)
        {
            return StatusCode(userFriendlyException.StatusCode, new { error = userFriendlyException.Message });
        }
        
        return StatusCode(500, new { error = "An unexpected error occurred." });
    }
}