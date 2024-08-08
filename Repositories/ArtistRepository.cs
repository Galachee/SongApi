using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Models;
using SongApi.Repositories.Contracts;

namespace SongApi.Repositories;

public class ArtistRepository : IArtistRepository 
{
    private readonly AppDbContext _context;

    public ArtistRepository(AppDbContext context)
        => _context = context;

    public async Task<List<Artist>> GetAllAsync()
    {
        var artists = await _context.Artists.AsNoTracking().ToListAsync();
        return artists;
    }

    public async Task<Artist?> GetByIdAsync(int id)
    {
        var artist = await _context.Artists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return artist;
    }

    public async Task AddAsync(Artist artist)
    {
        await _context.Artists.AddAsync(artist);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Artist artist)
    {
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Artist artist)
    {
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync();
    }
}