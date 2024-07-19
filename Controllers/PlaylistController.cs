using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.ViewModels;
using SongApi.ViewModels.Playlists;

namespace SongApi.Controllers;

[ApiController]

public class PlaylistController : ControllerBase
{
    [HttpGet("v1/playlists")]
    public async Task<IActionResult> GetPlaylists(
        [FromServices] AppDbContext context)
    {
        var playlists = await context.Playlists.
            Include(x=>x.User).
            AsNoTracking().
            Select(x=> new
            {
                x.Id,
                x.Title,
                x.User.Email
            }).
            ToListAsync();
        return Ok(new ResultViewModel<dynamic>(playlists));
    }

    [Authorize]
    [HttpPost("v1/playlists")]
    public async Task<IActionResult> CreatePlaylist(
        [FromServices] AppDbContext context,
        [FromBody] EditorPlaylistViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var userEmail = User.GetUserEmail();
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

        if (user is null)
            return NotFound(new ResultViewModel<string>("Erro ao encontrar usuário logado"));
        var playlist = new Playlist
        {
            Id = 0,
            Title = model.Title,
            User = user
        };
        
        try
        {
            await context.Playlists.AddAsync(playlist);
            await context.SaveChangesAsync();
            return Created("v1/playlists", new ResultViewModel<dynamic>(new
            {
                playlist.Id,
                playlist.Title,
                playlist.User.Email,
                playlist.Songs
            }));
        }
        catch (DbException)
        {
            return StatusCode(500,new ResultViewModel<string>("Erro ao inserir playlist no banco"));
        }
    }
}