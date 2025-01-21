using PTTS.API.Middleware;
using PTTS.API.Middlewares;
using PTTS.Application;
using PTTS.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
    app.ApplyMigrations();
}

// Group Identity API routes under "/auth"
// var authGroup = app.MapGroup("/auth").WithTags("Authentication");
// authGroup.MapIdentityApi<User>();

// app.UseHttpsRedirection();
app.MapGet("/", () => "API is live!");
app.UseAuthorization();
app.MapControllers();
app.Run();
