using System.ComponentModel.DataAnnotations;

namespace SongApi.ViewModels.PlaylistsViewModel;

public class EditorPlaylistViewModel
{
    [Required(ErrorMessage = "O título da playlist é obrigatório")]
    [MinLength(3,ErrorMessage = "O título deve conter no minímo 3 caracteres")]
    [MaxLength(500,ErrorMessage = "O título deve conter no máximo 500 caracteres")]
    public string Title { get; set; } = null!;
}