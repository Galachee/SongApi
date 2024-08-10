using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.ArtistsViewModel;

namespace SongApi.Endpoints.ArtistEndpoints;

public class UpdateArtistEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", async (IArtistRepository repo, EditorArtistViewModel model, int id) =>
        {
            var validationResult = model.ValidateAndReturnErrors();
            if (validationResult != null)
                return validationResult;
            
            var artist = await repo.GetByIdAsync(id);
            if (artist == null)
                return Results.NotFound(new ResultViewModel<string>("Artista não encontrado"));

            artist.Name = model.Name;
            artist.Bio = model.Bio;

            await repo.UpdateAsync(artist);
            return Results.Ok(new ResultViewModel<Artist>(artist));
        })
        .WithDescription("Atualizar um artista existente");
    }
}