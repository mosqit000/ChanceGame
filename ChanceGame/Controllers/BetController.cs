using System.Security.Authentication;
using System.Security.Claims;
using ChanceGame.CustomExceptions;
using ChanceGame.Models;
using ChanceGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChanceGame.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class BetController : ControllerBase
{
    private readonly IBetService _betService;

    public BetController(IBetService betService)
    {   
        _betService = betService;
    }


    [Authorize]
    [HttpPost("placeBet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BetResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult PlaceBet([FromBody] BetRequest request)
    {
        try
        {
            BetResponse response = _betService.PlaceBetAsync(request,
                User.Identity.Name).Result;
            return Ok(response);
        }
        catch (AggregateException exception)
        {
            foreach (var ex in exception.InnerExceptions)
            {
                if (ex is InsufficientBalanceException insufficientBalanceException)
                {
                    return Conflict(
                        new
                        {
                            error = "InsufficientBalance",
                            message = insufficientBalanceException.Message,
                            currentBalance = insufficientBalanceException.CurrentBalance
                        }
                    );
                }
                if (ex is InvalidCredentialException invalidCredentialException)
                {
                    return Unauthorized(
                        new
                        {
                            error = "InvalidCredentials",
                            message = invalidCredentialException.Message
                        }
                    );
                }
                
                Console.WriteLine(exception.Message);
              
            }
            return BadRequest(exception.Message);
        }
    }
    
}