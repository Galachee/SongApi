using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;

namespace SongApi.Endpoints.SongEndpoints;

public class GetAllSongsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISongRepository repo, int page = 0, int pageSize = 25) =>
            {
                var songs = await repo.GetAllAsync(page, pageSize);
                return Results.Ok(new ResultViewModel<List<Song>>(songs));
            })
            .WithName("Songs: Get All")
            .WithSummary("Recupera todas músicas");
    }
}