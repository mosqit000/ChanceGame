using ChanceGame.Models;
using ChanceGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChanceGame.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    
    private readonly IJwtService _jwtService;

    public AuthenticationController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] User loginUser)
    {
        if (!_jwtService.CheckUser(loginUser))
        {
            return Unauthorized("Invalid credentials.");
        }
        var token = _jwtService.GenerateToken(loginUser);
        return Ok(new { token });
    }

}