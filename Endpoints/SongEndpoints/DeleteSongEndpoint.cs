using SongApi.Common;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;

namespace SongApi.Endpoints.SongEndpoints;

public class DeleteSongEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}",
                async (ISongRepository repo, int id) =>
                {
                    var song = await repo.GetByIdAsync(id);
                    if (song == null)
                        return Results.NotFound(new ResultViewModel<string>("Musica não encontrada"));

                    await repo.DeleteAsync(song);
                    return Results.NoContent();
                })
            .WithDescription("Excluí uma música existente");
    }
}