using SongApi.Common;
using SongApi.Enums;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.SongsViewModel;

namespace SongApi.Endpoints.SongEndpoints;

public class CreateSongEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Songs : Create")
            .WithSummary("Adicionar uma nova música");
    }

    private static async Task<IResult> HandleAsync(ISongRepository repo, IArtistRepository artistRepo,
        EditorSongViewModel model)
    {
        var validationResult = model.ValidateAndReturnErrors();
        if (validationResult != null)
            return validationResult;
        var artist = await artistRepo.GetByIdAsync(model.ArtistId);
        if (artist == null)
            return Results.NotFound(new ResultViewModel<string>("Artista não encontrado"));
        var song = new Song()
        {
            Title = model.Title,
            DurationInMinutes = model.DurationInMinutes,
            Genre = (EGenre)model.GenreId,
            ArtistId = model.ArtistId
        };
        await repo.CreateAsync(song);
        return Results.Created("/songs", new ResultViewModel<Song>(song));
    }
}