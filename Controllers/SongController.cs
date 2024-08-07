using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Enums;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.ViewModels;
using SongApi.ViewModels.Songs;

namespace SongApi.Controllers;

[ApiController]
// [Authorize(Roles = "user")]
public class SongController : ControllerBase
{
    private readonly AppDbContext _context;

    public SongController(AppDbContext context)
        => _context = context;
    
    [HttpGet("v1/songs")]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int page = 0, int pageSize = 25)
    {
        var songs = await _context.Songs.AsNoTracking()
            .Skip(pageSize * page)
            .Take(pageSize)
            .OrderBy(x => x.Id)
            .ToListAsync();

        return Ok(new ResultViewModel<List<Song>>(songs));
    }


    [HttpGet("v1/songs/genre/{genre:int}")]
    public async Task<IActionResult> GetByGenreAsync(
        [FromRoute] int genre)
    {
        var songs = await _context.Songs.AsNoTracking()
            .Where(x => x.Genre == (EGenre)genre)
            .Include(x => x.Artist)
            .Select(song => new
            {
                song.Id,
                Name = song.Title,
                Genre = song.Genre.ToString(),
                Singer = song.Artist.Name
            })
            .OrderBy(x => x.Id)
            .ToListAsync();

        return Ok(new ResultViewModel<dynamic>(songs));
    }

    [HttpPost("v1/songs")]
    public async Task<IActionResult> CreateSongAsync(
        [FromBody] EditorSongViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == model.ArtistId);
        if (artist == null)
            return BadRequest(new ResultViewModel<string>("Artista inválido"));

        var song = new Song
        {
            Id = 0,
            Title = model.Title,
            DurationInMinutes = model.DurationInMinutes,
            Genre = (EGenre)model.Genre,
            Artist = artist
        };

        await _context.Songs.AddAsync(song);
        await _context.SaveChangesAsync();

        return Created("v1/songs", new ResultViewModel<Song>(song));
    }

    [HttpPut("v1/songs/{id:int}")]
    public async Task<IActionResult> UpdateSongAsync(
        [FromRoute] int id,
        [FromBody] EditorSongViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        if (song == null)
            return NotFound(new ResultViewModel<string>("Musica não encontrada"));
        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == model.ArtistId);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));


        song.Title = model.Title;
        song.DurationInMinutes = model.DurationInMinutes;
        song.Genre = (EGenre)model.Genre;
        song.Artist = artist;

        _context.Songs.Update(song);
        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<Song>(song));
    }

    [HttpDelete("v1/songs/{id:int}")]
    public async Task<IActionResult> DeleteSongAsync(
        [FromRoute] int id)
    {
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        if (song == null)
            return NotFound(new ResultViewModel<string>("Musica não encontrada"));

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();
        return Ok(new ResultViewModel<dynamic>(new
        {
            Message = $"Musica {song.Title} excluída com sucesso"
        }));
    }
}