using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SongApi.Enums;

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