using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels.PlaylistsViewModel;

namespace SongApi.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly AppDbContext _context;

    public PlaylistRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<GetPlaylistViewModel>> GetAllAsync()
    {
        var playlists = await _context.Playlists.
            Include(x=>x.User).
            AsNoTracking().
            Select(x=> new GetPlaylistViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                User = x.User.Email
            }).
            ToListAsync();
        return playlists;
    }

    public async Task<Playlist?> GetByIdAsync(int id)
    {
        var playlist = await _context.Playlists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return playlist;
    }

    public async Task AddAsync(Playlist playlist)
    {
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Playlist playlist)
    {
        _context.Playlists.Update(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Playlist playlist)
    {
        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();
    }
}