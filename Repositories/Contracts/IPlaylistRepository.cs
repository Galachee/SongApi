using SongApi.Models;
using SongApi.ViewModels.PlaylistsViewModel;

namespace SongApi.Repositories.Contracts;

public interface IPlaylistRepository
{
    Task<List<GetPlaylistViewModel>> GetAllAsync();
    Task<Playlist?> GetByIdAsync(int id);
    Task AddAsync(Playlist playlist);
    Task UpdateAsync(Playlist playlist);
    Task DeleteAsync(Playlist playlist);
}