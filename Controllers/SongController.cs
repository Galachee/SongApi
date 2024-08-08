using Microsoft.AspNetCore.Mvc;
using SongApi.Enums;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.Songs;

namespace SongApi.Controllers;

[ApiController]
// [Authorize(Roles = "user")]
public class SongController : ControllerBase
{
    private readonly ISongRepository _repository;
    private readonly IArtistRepository _artistRepository;

    public SongController(ISongRepository repository, IArtistRepository artistRepository)
    {
        _repository = repository;
        _artistRepository = artistRepository;
    }

    [HttpGet("v1/songs")]
    public async Task<IActionResult> GetAllSongsAsync(
        [FromQuery] int page = 0, int pageSize = 25)
    {
        var songs = await _repository.GetAllAsync(page, pageSize);
        return Ok(new ResultViewModel<List<Song>>(songs));
    }

    [HttpGet("v1/songs/{id:int}")]
    public async Task<IActionResult> GetSongByIdAsync(
        [FromRoute] int id)
    {
        var song = await _repository.GetByIdAsync(id);

        if (song == null)
            return NotFound(new ResultViewModel<string>("Musica não encontrada"));
        return Ok(new ResultViewModel<Song>(song));
    }

    [HttpPost("v1/songs")]
    public async Task<IActionResult> AddSongAsync(
        [FromBody] EditorSongViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var artist = await _artistRepository.GetByIdAsync(model.ArtistId);
        if (artist == null)
            return BadRequest(new ResultViewModel<string>("Artista inválido"));

        var song = new Song
        {
            Title = model.Title,
            DurationInMinutes = model.DurationInMinutes,
            Genre = (EGenre)model.GenreId,
            ArtistId = model.ArtistId
        };
        
        await _repository.CreateAsync(song);
        return Created("v1/songs", new ResultViewModel<Song>(song));
    }

    [HttpPut("v1/songs/{id:int}")]
    public async Task<IActionResult> UpdateSongAsync(
        [FromRoute] int id,
        [FromBody] EditorSongViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var song = await _repository.GetByIdAsync(id);
        if (song == null)
            return NotFound(new ResultViewModel<string>("Musica não encontrada"));

        var artist = await _artistRepository.GetByIdAsync(model.ArtistId);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));


        song.Title = model.Title;
        song.DurationInMinutes = model.DurationInMinutes;
        song.Genre = (EGenre)model.GenreId;
        song.Artist = artist;

        await _repository.UpdateAsync(song);
        return Ok(new ResultViewModel<Song>(song));
    }

    [HttpDelete("v1/songs/{id:int}")]
    public async Task<IActionResult> DeleteSongAsync(
        [FromRoute] int id)
    {
        var song = await _repository.GetByIdAsync(id);
        if (song == null)
            return NotFound(new ResultViewModel<string>("Musica não encontrada"));


        await _repository.DeleteAsync(song);
        return Ok(new ResultViewModel<dynamic>(new
        {
            Message = $"Musica {song.Title} excluída com sucesso"
        }));
    }
}