using SongApi.Models;

namespace SongApi.Repositories.Contracts;

public interface IUserRepository
{
    Task Save(User user);
    Task<User?> GetUserByEmail(string email);
}