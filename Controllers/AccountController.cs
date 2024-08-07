using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SongApi.Data;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.Services.Contracts;
using SongApi.ViewModels;
using SongApi.ViewModels.Users;

namespace SongApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }


    [HttpPost("v1/accounts/register")]
    public async Task<IActionResult> Register(
        RegisterUserViewModel model)
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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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
    public async Task<IActionResult> Login(
        LoginUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = await _context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário não encontrado"));

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = _tokenService.GenerateToken(user);
            return Ok(new ResultViewModel<dynamic>(new
            {
                token
            }));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno do servidor"));
        }
    }
}