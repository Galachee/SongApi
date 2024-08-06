using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.ViewModels;
using SongApi.ViewModels.Artists;

namespace SongApi.Controllers;

[ApiController]
public class ArtistController : ControllerBase
{
    private readonly AppDbContext _context;

    public ArtistController(AppDbContext context) 
        => _context = context;

    [HttpGet("v1/artists")]
    public async Task<IActionResult> GetAllAsync()
    {
        var artists = await _context.Artists.AsNoTracking()
            .ToListAsync();
        return Ok(new ResultViewModel<List<Artist>>(artists));
    }

    [HttpGet("v1/artists/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var artist = await _context.Artists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));

        return Ok(new ResultViewModel<Artist>(artist));
    }

    [HttpPost("v1/artists")]
    public async Task<IActionResult> AddArtistAsync(
        [FromBody] EditorArtistViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var artist = new Artist()
        {
            Name = model.Name,
            Bio = model.Bio
        };

        await _context.Artists.AddAsync(artist);
        await _context.SaveChangesAsync();
        return Created("v1/artists", new ResultViewModel<Artist>(artist));
    }

    [HttpPut("v1/artists/{id:int}")]
    public async Task<IActionResult> UpdateArtistAsync(
        [FromRoute] int id,
        [FromBody] EditorArtistViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));

        artist.Name = model.Name;
        artist.Bio = model.Bio;
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync();

        return Ok(new ResultViewModel<Artist>(artist));
    }

    [HttpDelete("v1/artists/{id:int}")]
    public async Task<IActionResult> DeleteArtistAsync([FromRoute] int id)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);

        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));

        _context.Artists.Remove(artist);

        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<dynamic>(new
        {
            Message = $"Artista {artist.Name} excluído com sucesso",
            Artist = artist
        }));
    }
}