using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChanceGame.Configurations;
using ChanceGame.Database;
using ChanceGame.Security;
using ChanceGame.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User = ChanceGame.Models.User;


namespace ChanceGame.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserContext _userContext;
    
    public JwtService(IOptions<JwtSettings> jwtSettings,UserContext userContext)
    {
        _jwtSettings = jwtSettings.Value;
        _userContext = userContext;
    }

    public bool CheckUser(User loginUser)
    {
        var user = _userContext.Users.SingleOrDefault(u => u.Username == loginUser.Username);
        return user != null && SecurityUtils.VerifyPassword(loginUser.Password, user.Password);
    }

    public  string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}