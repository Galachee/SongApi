using SongApi.Common;
using SongApi.Endpoints.AccountEndpoints;
using SongApi.Endpoints.ArtistEndpoints;
using SongApi.Endpoints.PlaylistEndpoints;
using SongApi.Endpoints.SongEndpoints;

namespace SongApi.Extensions;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new {message = "OK"});

        endpoints.MapGroup("/v1/songs")
            .WithTags("Songs")
            .MapEndpoint<GetAllSongsEndpoint>()
            .MapEndpoint<GetSongByIdEndpoint>()
            .MapEndpoint<CreateSongEndpoint>()
            .MapEndpoint<UpdateSongEndpoint>()
            .MapEndpoint<DeleteSongEndpoint>();

        endpoints.MapGroup("/v1/artists")
            .WithTags("Artists")
            .MapEndpoint<GetAllArtistsEndpoint>()
            .MapEndpoint<GetArtistByIdEndpoint>()
            .MapEndpoint<CreateArtistEndpoint>()
            .MapEndpoint<UpdateArtistEndpoint>()
            .MapEndpoint<DeleteArtistEndpoint>();

        endpoints.MapGroup("v1/account")
            .WithTags("Account")
            .MapEndpoint<AccountRegisterEndpoint>()
            .MapEndpoint<AccountLoginEndpoint>();

        endpoints.MapGroup("v1/playlists")
            .WithTags("Playlists")
            .MapEndpoint<GetAllPlaylistsEndpoint>()
            .MapEndpoint<CreatePlaylistEndpoint>();
    }
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}