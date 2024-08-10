using System.ComponentModel.DataAnnotations;

namespace SongApi.ViewModels.UsersViewModel;

public class LoginUserViewModel
{
    [Required (ErrorMessage = "O email é obrigatório")] 
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A senha é obrigatória")] 
    public string Password { get; set; } = null!;
}