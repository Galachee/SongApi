using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.Repositories.Contracts;
using SongApi.Services.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.Users;

namespace SongApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;
    public AccountController(IUserRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }
    
    [HttpPost("v1/accounts/register")]
    public async Task<IActionResult> Register(RegisterUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

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
            await _repository.Save(user);
            return Created("v1/accounts", new ResultViewModel<dynamic>(new
            {
                user = user.Email, password,
                Message = "Usuário cadastrado com sucesso!"
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro ao inserir o usuário"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno do servidor"));
        }
    }

    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login(LoginUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var user = await _repository.GetUserByEmail(model.Email);
        if (user == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário não encontrado"));

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));
        
        try
        {
            var token = _tokenService.GenerateToken(user);
            return Ok(new ResultViewModel<dynamic>(new
            {
                token = token
            }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }
}