using SongApi.Data;

namespace SongApi.Extensions;

public static class DataBaseManagement
{
    public static void MigrateInitialization(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDb = serviceScope.ServiceProvider.GetService<AppDbContext>();
            
            serviceDb!.Database.EnsureCreated();
        }
    }
}