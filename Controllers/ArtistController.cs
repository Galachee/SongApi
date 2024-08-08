using Microsoft.AspNetCore.Mvc;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.Artists;

namespace SongApi.Controllers;

[ApiController]
public class ArtistController : ControllerBase
{
    private readonly IArtistRepository _repository;
    public ArtistController(IArtistRepository repository) 
        => _repository = repository;

    [HttpGet("v1/artists")]
    public async Task<IActionResult> GetAllAsync()
    {
        var artists = await _repository.GetAllAsync();
        return Ok(new ResultViewModel<List<Artist>>(artists));
    }

    [HttpGet("v1/artists/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var artist = await _repository.GetByIdAsync(id);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));
        return Ok(new ResultViewModel<Artist>(artist));
    }

    [HttpPost("v1/artists")]
    public async Task<IActionResult> AddAsync(
        [FromBody] EditorArtistViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var artist = new Artist()
        {
            Name = model.Name,
            Bio = model.Bio
        };
        await _repository.AddAsync(artist);
        return Created("v1/artists", new ResultViewModel<Artist>(artist));
    }

    [HttpPut("v1/artists/{id:int}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] EditorArtistViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var artist = await _repository.GetByIdAsync(id);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));
        
        artist.Name = model.Name;
        artist.Bio = model.Bio;
        
        await _repository.UpdateAsync(artist);
        return Ok(new ResultViewModel<Artist>(artist));
    }

    [HttpDelete("v1/artists/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var artist = await _repository.GetByIdAsync(id);
        if (artist == null)
            return NotFound(new ResultViewModel<string>("Artista não encontrado"));
        
        
        await _repository.DeleteAsync(artist);
        return Ok(new ResultViewModel<dynamic>(new
        {
            Message = $"Artista {artist.Name} excluído com sucesso",
            Artist = artist
        }));
    }
}