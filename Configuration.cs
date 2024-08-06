namespace SongApi;

public static class Configuration
{
    public static class SecretsKeys
    {
        public static byte[] JwtKey { get; set; } = null!;
    }

    public static class DatabaseConfiguration
    {
        public static string? ConnectionString { get; set; }
    }
}