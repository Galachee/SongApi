using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// User-Secrets
builder.SetSecretKeys();

//Add Configurations API
builder.AddConfiguration();

//Configuration Authentication and Authorization with JWT Token
builder.ConfigureAuthentication();
builder.ConfigureAuthorization();

//Documentation with Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token JWT para autenticação"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }   
    });
    
});

var app = builder.Build();
app.MigrateInitialization();

app.UseAuthentication();
app.UseAuthorization();


app.MapEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();