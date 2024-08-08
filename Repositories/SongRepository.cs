using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Models;
using SongApi.Repositories.Contracts;

namespace SongApi.Repositories;

public class SongRepository : ISongRepository
{
    private readonly AppDbContext _context;

    public SongRepository(AppDbContext context)
        => _context = context;
    
    public async Task<List<Song>> GetAllAsync(int page = 0, int pageSize = 25)
    {
        var songs = await _context.Songs.AsNoTracking()
            .Skip(pageSize * page)
            .Take(pageSize)
            .OrderBy(x => x.Id)
            .ToListAsync();
        return songs;
    }

    public async Task<Song?> GetByIdAsync(int id)
    {
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        return song;
    }

    public async Task CreateAsync(Song song)
    {
        await _context.Songs.AddAsync(song);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Song song)
    {
        _context.Songs.Update(song);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Song song)
    {
        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();
    }
}