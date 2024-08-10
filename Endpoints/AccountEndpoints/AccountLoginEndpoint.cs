using SecureIdentity.Password;
using SongApi.Common;
using SongApi.Repositories.Contracts;
using SongApi.Services.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.UsersViewModel;

namespace SongApi.Endpoints.AccountEndpoints;

public class AccountLoginEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (IUserRepository repository,LoginUserViewModel model,ITokenService tokenService) =>
        {
            var validationResult = model.ValidateAndReturnErrors();
            if (validationResult != null)
                return validationResult;
            
            var user = await repository.GetUserByEmail(model.Email);
            if (user == null)
                return Results.NotFound(new ResultViewModel<string>("Usuário não encontrado"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return Results.Unauthorized();
        
            try
            {
                var token = tokenService.GenerateToken(user);
                return Results.Ok(new ResultViewModel<dynamic>(new
                {
                    token = token
                }));
            }
            catch (Exception)
            {
                return Results.StatusCode(500);
            }
        }).WithDescription("Realizar login com Email e Senha");
    }
}