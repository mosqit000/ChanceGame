using System.Security.Authentication;
using ChanceGame.Database;
using ChanceGame.Models;
using ChanceGame.Services.Interfaces;
using ChanceGame.CustomExceptions;
namespace ChanceGame.Services.Implementations;

public class BetService : IBetService
{
    private readonly UserContext _userContext;

    public BetService(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<BetResponse> PlaceBetAsync(BetRequest request, string userId)
    {

        var user = _userContext.Users
            .FirstOrDefault(u => u.Username.Equals(userId));

        if (user == null) throw new InvalidCredentialException(); // double check user
        
        // check if user has sufficient balance to place a bet
        if (user.Balance < request.Points || user.Balance == 0)
        {
            throw new InsufficientBalanceException(user.Balance);
        }

        Random rng = new Random();
        int rand = rng.Next(10); // 0 -> 9
        bool won = request.Number == rand;
        
        user.Balance = won ?  user.Balance + (9 * request.Points): user.Balance - request.Points;
        _userContext.Users.Update(user);
        await _userContext.SaveChangesAsync().ConfigureAwait(false);

            return won?  new BetResponse()
            {
                Points = "+"+request.Points * 9 ,
                Account = user.Balance,
                Status = "won" 
 
            }: new BetResponse()
            {
                Points = "-" + request.Points,
                Status = "lost",
                Account = null
            };
       
    }
    
}