using ChanceGame.Models;

namespace ChanceGame.Services.Interfaces;

public interface IBetService
{
    Task<BetResponse> PlaceBetAsync(BetRequest request,string userId);
}