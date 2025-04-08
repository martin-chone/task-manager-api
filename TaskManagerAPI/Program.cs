using TaskManagerAPI.Extensions;
using TaskManagerAPI.Mappings;
using TaskManagerAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Charge les configurations spécifiques à l'environnement
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Host.ConfigureSerilog();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddAppServices();


var app = builder.Build();

// Middleware d'erreur global
app.UseMiddleware<ExceptionMiddleware>();

// Middleware pour Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pour authentification et autorisation
app.UseAuthentication();
app.UseAuthorization();

// Middleware pour route des contrôleurs
app.MapControllers();

app.Run();