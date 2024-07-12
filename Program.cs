using SongApi.Extensions;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();