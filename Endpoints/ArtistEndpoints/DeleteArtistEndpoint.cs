using SongApi.Common;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;

namespace SongApi.Endpoints.ArtistEndpoints;

public class DeleteArtistEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", async (IArtistRepository repo, int id) =>
        {
            var artist = await repo.GetByIdAsync(id);
            if (artist == null)
                return Results.NotFound(new ResultViewModel<string>("Artista não encontrado"));

            await repo.DeleteAsync(artist);
            return Results.NoContent();
        })
        .WithDescription("Excluir um artista");
    }
}