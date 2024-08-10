using System.ComponentModel.DataAnnotations;

namespace SongApi.ViewModels.UsersViewModel;

public class RegisterUserViewModel
{
    
    [Required]
    [MinLength(3,ErrorMessage = "O username deve conter no minimo 3 caracteres")]
    public string Username { get; set; } = null!;
    
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(4,ErrorMessage = "A senha deve conter no minímo 3 caracteres")]
    public string Password { get; set; } = null!;

}   