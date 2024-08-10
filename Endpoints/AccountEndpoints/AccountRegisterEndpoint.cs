using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SongApi.Common;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.UsersViewModel;

namespace SongApi.Endpoints.AccountEndpoints;

public class AccountRegisterEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (IUserRepository repository,RegisterUserViewModel model) =>
        {
            var validationResult = model.ValidateAndReturnErrors();
            if (validationResult != null)
                return validationResult;
            
            var user = new User()
            {
                Id = 0,
                Username = model.Username,
                Email = model.Email
            };

            var password = model.Password;
            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await repository.Save(user);
                return Results.Created("v1/accounts", new ResultViewModel<dynamic>(new
                {
                    user = user.Email, password,
                    Message = "Usuário cadastrado com sucesso!"
                }));
            }
            catch (DbUpdateException)
            {
                return Results.StatusCode(500);
            }
            catch (Exception)
            {
                return Results.StatusCode(500);
            }
        }).WithDescription("Registrar novo usuário");
    }
}