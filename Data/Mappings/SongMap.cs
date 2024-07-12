using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SongApi.Models;

namespace SongApi.Data.Mappings;

public class SongMap : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.ToTable("Song");

        // Chave primária
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();


        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120);

        // Relação Genero
        builder.Property(x => x.Genre).HasConversion<int>().IsRequired();
        
        builder.Property(x => x.DurationInMinutes)
            .HasColumnName("DurationInMinutes")
            .HasColumnType("DECIMAL(5,2)")
            .IsRequired();

        builder.HasOne(x => x.Artist)
            .WithMany(x => x.Songs)
            .HasConstraintName("FK_Song_ArtistId")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.Title, "IX_Song_Title");
        
    }
}