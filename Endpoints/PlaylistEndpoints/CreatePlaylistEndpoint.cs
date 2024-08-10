using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.PlaylistsViewModel;

namespace SongApi.Endpoints.PlaylistEndpoints;

public class CreatePlaylistEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (HttpContext httpContext, IPlaylistRepository repository, IUserRepository userRepository, EditorPlaylistViewModel model) =>
        {
            var validationResult = model.ValidateAndReturnErrors();
            if (validationResult != null)
                return validationResult;
            
            var userEmail = httpContext.User.GetUserEmail();
            var user = await userRepository.GetUserByEmail(userEmail);
            
            if (user == null)
                return Results.Unauthorized();
            
            var playlist = new Playlist
            {
                Id = 0,
                Title = model.Title,
                User = user
            };
            try
            {
                await repository.AddAsync(playlist);
                return Results.Created("v1/playlists", new ResultViewModel<dynamic>(new
                {
                    playlist.Id,
                    playlist.Title,
                    playlist.User.Email,
                    playlist.Songs
                }));
            }
            catch (Exception)
            {
                return Results.StatusCode(500);
            }
        }).RequireAuthorization();
    }
}