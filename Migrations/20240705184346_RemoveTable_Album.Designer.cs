﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SongApi.Data;

#nullable disable

namespace SongApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240705184346_RemoveTable_Album")]
    partial class RemoveTable_Album
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SongApi.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT")
                        .HasColumnName("Bio");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("SongApi.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<decimal>("DurationInMinutes")
                        .HasColumnType("DECIMAL(5,2)")
                        .HasColumnName("DurationInMinutes");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex(new[] { "Title" }, "IX_Song_Title");

                    b.ToTable("Song", (string)null);
                });

            modelBuilder.Entity("SongApi.Models.Song", b =>
                {
                    b.HasOne("SongApi.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Song_ArtistId");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("SongApi.Models.Artist", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
