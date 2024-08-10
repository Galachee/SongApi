using SongApi.Common;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.PlaylistsViewModel;

namespace SongApi.Endpoints.PlaylistEndpoints;

public class GetAllPlaylistsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IPlaylistRepository repository) =>
        {
            var playlists = await repository.GetAllAsync();
            return Results.Ok(new ResultViewModel<List<GetPlaylistViewModel>>(playlists));
        });
    }
}