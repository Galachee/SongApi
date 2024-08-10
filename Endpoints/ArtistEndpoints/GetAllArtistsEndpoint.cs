using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;

namespace SongApi.Endpoints.ArtistEndpoints;

public class GetAllArtistsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IArtistRepository repo) =>
        {
            var artists = await repo.GetAllAsync();
            
            return Results.Ok(new ResultViewModel<List<Artist>>(artists));
        })
        .WithDescription("Recuperar todos artistas");
    }
}