using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.ArtistsViewModel;

namespace SongApi.Endpoints.ArtistEndpoints;

public class CreateArtistEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (IArtistRepository repo, EditorArtistViewModel model) =>
        {
            var validationResult = model.ValidateAndReturnErrors();
            if (validationResult != null)
                return validationResult;

            var artist = new Artist()
            {
                Name = model.Name,
                Bio = model.Bio
            };
            await repo.AddAsync(artist);
            return Results.Created("/artists", new ResultViewModel<Artist>(artist));
        })
        .WithDescription("Adicionar um novo artista");
    }
}