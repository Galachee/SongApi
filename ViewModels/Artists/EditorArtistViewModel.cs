using System.ComponentModel.DataAnnotations;

namespace SongApi.ViewModels.Artists;

public class EditorArtistViewModel
{
    
    [Required(ErrorMessage = "O nome do artista é obrigatório")]
    public string Name { get; set; } = null!;

    
    [Required(ErrorMessage = "A bio do artista é obrigátoria")] 
    [MinLength(5,ErrorMessage = "A bio deve conter entre 5 e 3000 caracteres")]
    [MaxLength(3000,ErrorMessage = "A bio deve conter entre 5 e 3000 caracteres")]
    public string Bio { get; set; } = null!;
}