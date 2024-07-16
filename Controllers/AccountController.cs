using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SongApi.Data;
using SongApi.Extensions;
using SongApi.Models;
using SongApi.Services;
using SongApi.ViewModels;
using SongApi.ViewModels.Users;

namespace SongApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
   [HttpPost("v1/accounts/register")]
   public async Task<IActionResult> Register(
      [FromServices] AppDbContext context,
      [FromBody] RegisterUserViewModel model)
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
         
         await context.Users.AddAsync(user); 
         await context.SaveChangesAsync();
         return Ok(new ResultViewModel<dynamic>(new
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
      [FromServices] AppDbContext context,
      [FromBody] LoginUserViewModel model,
      [FromServices] TokenService tokenService)
   {
      if (!ModelState.IsValid)
         return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

      var user = await context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == model.Email);
      if (user == null)
         return StatusCode(401,new ResultViewModel<string>("Usuário não encontrado"));

      if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
         return StatusCode(401,new ResultViewModel<string>("Usuário ou senha inválidos"));

      try
      {
         var token = tokenService.GenerateToken(user);
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