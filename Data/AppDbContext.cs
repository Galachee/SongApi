using Microsoft.EntityFrameworkCore;
using SongApi.Data.Mappings;
using SongApi.Models;

namespace SongApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Song> Songs { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<User> Users { get; set; }    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SongMap());
        modelBuilder.ApplyConfiguration(new ArtistMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}