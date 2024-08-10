namespace SongApi.ViewModels.PlaylistsViewModel;

public class GetPlaylistViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string User { get; set; } = null!;
}