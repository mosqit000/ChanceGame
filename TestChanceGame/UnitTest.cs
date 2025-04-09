using System.Security.Authentication;
using ChanceGame.CustomExceptions;
using ChanceGame.Models;

namespace TestChanceGame;
using Xunit;
using FluentAssertions;
using ChanceGame.Services.Implementations;
using ChanceGame.Database.Entities;
using ChanceGame.Database;
using Microsoft.EntityFrameworkCore;

public class UnitTest
{
    private UserContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new UserContext(options);
    }   
    
    [Fact]
    public async Task PlaceBetAsync_ShouldThrow_WhenNotAuthorized()
    {
        var context = GetInMemoryContext();
        var service = new BetService(context);

        var request = new BetRequest
        {
            Points = 100,
            Number = 5 
        };
        
        var result = async () => await service.PlaceBetAsync(request, "testuser");


        await result.Should()
            .ThrowAsync<InvalidCredentialException>();
    }
    
    
    [Fact]
    public async Task PlaceBetAsync_ShouldReturnSuccess_WhenBalanceIsSufficient()
    {
        var context = GetInMemoryContext();
        var user = new User { Username = "testuser",Password = "testuser",Balance = 1000 };
        context.Users.Add(user);
        context.SaveChanges();

        var service = new BetService(context);

        var request = new BetRequest
        {
            Points = 100,
            Number = 5 
        };
        
        var result = await service.PlaceBetAsync(request, "testuser");
        
        result.Should().NotBeNull();
        result.Status.Should().BeOneOf("won", "lost"); 
    }
    
    [Fact]
    public async Task PlaceBetAsync_ShouldThrow_WhenBalanceTooLow()
    {
        var context = GetInMemoryContext();
        var user = new User { Username = "testuser",Password = "testuser", Balance = 50 };
        context.Users.Add(user);
        context.SaveChanges();

        var service = new BetService(context);

        var request = new BetRequest
        {
            Points = 100,
            Number = 4
        };
        
        var act = async () => await service.PlaceBetAsync(request, "testuser");
        
        await act.Should()
            .ThrowAsync<InsufficientBalanceException>()
            .WithMessage("Insufficient balance*");
    }
}
    
