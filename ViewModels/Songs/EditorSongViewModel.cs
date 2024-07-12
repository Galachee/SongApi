using System.ComponentModel.DataAnnotations;


namespace SongApi.ViewModels.Songs;

public class EditorSongViewModel
{
    [Required(ErrorMessage = "O Titulo é obrigatório")]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "A duração é obrigatória")]
    public decimal DurationInMinutes { get; set; }

    [Required(ErrorMessage = "O codigo do genero é obrigatorio")]
    public int Genre { get; set; }
    // public EGenre Genre { get; set; }
    
    [Required(ErrorMessage = "O código do artista é obrigatório")]
    public int ArtistId { get; set; } 
    
    
    //[Required(ErrorMessage = "O artista é obrigatorio")]
    //public Artist Artist { get; set; } = null!;
    
    
}