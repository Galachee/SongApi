using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SongApi.Models;

namespace SongApi.Data.Mappings;

public class PlaylistMap : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlist");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(x => x.Title).IsRequired().HasColumnName("Title").HasColumnType("NVARCHAR")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.Genre).HasConversion<int>().IsRequired(false);

        builder.HasMany(x => x.Songs)
            .WithMany(x => x.Playlists)
            .UsingEntity<Dictionary<string, object>>(
                "PlaylistSong",
                song => song
                    .HasOne<Song>()
                    .WithMany()
                    .HasForeignKey("SongId")
                    .HasConstraintName("FK_PlaylistSong_SongId")
                    .OnDelete(DeleteBehavior.Cascade),
                playlist => playlist
                    .HasOne<Playlist>()
                    .WithMany()
                    .HasForeignKey("PlaylistId")
                    .HasConstraintName("FK_PlaylistSong_PlaylistId")
                    .OnDelete(DeleteBehavior.Cascade));

    }
}