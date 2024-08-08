using Microsoft.AspNetCore.Mvc;
using SongApi.Models;
using SongApi.ViewModels;
using SongApi.ViewModels.Artists;

namespace SongApi.Repositories.Contracts;

public interface IArtistRepository
{
    
    Task<List<Artist>> GetAllAsync();
    Task<Artist?> GetByIdAsync(int id);
    Task AddAsync(Artist artist);
    Task UpdateAsync(Artist artist);
    Task DeleteAsync(Artist artist);
}