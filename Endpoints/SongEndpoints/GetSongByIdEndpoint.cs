using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;

namespace SongApi.Endpoints.SongEndpoints;

public class GetSongByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", async (ISongRepository repo, int id) =>
            {
                var song = await repo.GetByIdAsync(id);
                if (song == null)
                    return Results.NotFound(new ResultViewModel<string>("Musica não encontrada"));

                return Results.Ok(new ResultViewModel< Song>(song));
            })
            .WithDescription("Recupera uma musica pelo ID");
    }
}