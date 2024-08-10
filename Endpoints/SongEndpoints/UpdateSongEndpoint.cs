using SongApi.Common;
using SongApi.Enums;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.SongsViewModel;

namespace SongApi.Endpoints.SongEndpoints;

public class UpdateSongEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}",
            async (ISongRepository repo, IArtistRepository artistRepo, EditorSongViewModel model, int id) =>
            {
                var validationResult = model.ValidateAndReturnErrors();
                if (validationResult != null)
                    return validationResult;
                
                var song = await repo.GetByIdAsync(id);
                if (song == null)
                    return Results.NotFound(new ResultViewModel<string>("Musica não encontrada"));
                var artist = await artistRepo.GetByIdAsync(model.ArtistId);
                if (artist == null)
                    return Results.NotFound(new ResultViewModel<string>("Artista Inválido"));
                
                song.Title = model.Title;
                song.DurationInMinutes = model.DurationInMinutes;
                song.Genre = (EGenre)model.GenreId;
                song.Artist = artist;
                
                await repo.UpdateAsync(song);
                return Results.Ok(new ResultViewModel<Song>(song));
            })
            .WithDescription("Atualiza uma música existente");
    }
}