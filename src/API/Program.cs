using API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddJwtConfiguration(builder.Configuration);

Application.Dependencies.ConfigureServices(builder.Configuration, builder.Services);
Infra.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
