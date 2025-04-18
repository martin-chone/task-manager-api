using Serilog;
using TaskManager.API.Extensions;
using TaskManager.API.Middlewares;
using TaskManager.Application.Mappings;
using TaskManager.Composition.Extensions;
using TaskManager.Infrastructure.Mappings;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

//Mapping profiles
builder.Services.AddAutoMapper(
    typeof(ApplicationMappingProfile).Assembly,
    typeof(InfrastructureMappingProfile).Assembly);

//Composition
builder.Services.AddTaskManagerServices(builder.Configuration);

//Swagger Documentation
builder.Services.AddSwaggerDocumentation();

//Controllers
builder.Services.AddControllers();

var app = builder.Build();

//Global error handler
app.UseMiddleware<ExceptionMiddleware>();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Auth middlewares
app.UseAuthentication();
app.UseAuthorization();

//Routing
app.MapControllers();

app.Run();