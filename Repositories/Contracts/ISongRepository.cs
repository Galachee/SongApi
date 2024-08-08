using SongApi.Models;

namespace SongApi.Repositories.Contracts;

public interface ISongRepository
{
    Task<List<Song>> GetAllAsync(int page = 0, int pageSize = 25);

    Task<Song?> GetByIdAsync(int id);
    Task CreateAsync(Song song);
    Task UpdateAsync(Song song);
    Task DeleteAsync(Song song);
}