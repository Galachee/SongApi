
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SongApi.Data;
using SongApi.Repositories;
using SongApi.Repositories.Contracts;
using SongApi.Services;
using SongApi.Services.Contracts;

namespace SongApi.Extensions;

public static class BuilderExtension
{
    
    public static void SetSecretKeys(this WebApplicationBuilder builder)
    {
        var key = builder.Configuration.GetValue<string>("JwtKey");
        if (string.IsNullOrEmpty(key))
        {
            throw new SecurityException("Chave JWT vazia");
        }
        Configuration.SecretsKeys.JwtKey = Encoding.ASCII.GetBytes(key);
        Configuration.DatabaseConfiguration.ConnectionString =  builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");
    }
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddServices();
        builder.AddRepositories();
        builder.ConfigureMvc();
    }
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.DatabaseConfiguration.ConnectionString);
        });
        builder.Services.AddTransient<ITokenService,TokenService>();
    }

    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
        builder.Services.AddScoped<ISongRepository, SongRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
    public static void ConfigureMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });
    }
    
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Configuration.SecretsKeys.JwtKey),
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });
    }
    public static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
    }
}