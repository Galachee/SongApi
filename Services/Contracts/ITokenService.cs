using SongApi.Models;

namespace SongApi.Services.Contracts;

public interface ITokenService
{
    string GenerateToken(User user);
}