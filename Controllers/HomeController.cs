using Microsoft.AspNetCore.Mvc;

namespace SongApi.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    
    [HttpGet("/")]
    public IActionResult HealthCheck()
    {
        return Ok("HealthCheck");
    }
}