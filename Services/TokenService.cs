using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.Services.Contracts;

namespace SongApi.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Configuration.SecretsKeys.JwtKey;
        var claims = user.GetClaims();
        var credencials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credencials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = new ClaimsIdentity(claims)
        };  
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}