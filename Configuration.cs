namespace SongApi;

public static class Configuration
{
    
    public class SecretsKeys
    {
        public static byte[] JwtKey { get; set; } = null!;

    }
    public class DatabaseConfiguration
    {
        public static string ConnectionString { get; set; } = null!;
    }
}