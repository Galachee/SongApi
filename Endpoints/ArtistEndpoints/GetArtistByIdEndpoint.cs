using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;

namespace SongApi.Endpoints.ArtistEndpoints;

public class GetArtistByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", async (IArtistRepository repo, int id) =>
        {
            var artist = await repo.GetByIdAsync(id);
            if (artist == null)
                return Results.NotFound(new ResultViewModel<string>("Artista não encontrado"));

            return Results.Ok(new ResultViewModel<Artist>(artist));
        })
        .WithDescription("Recuperar uma música pelo ID");
    }
}