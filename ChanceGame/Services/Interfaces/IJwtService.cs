using ChanceGame.Models;

namespace ChanceGame.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    
    bool CheckUser(User loginUser);
}