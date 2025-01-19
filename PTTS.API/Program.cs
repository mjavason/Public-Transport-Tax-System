using Microsoft.AspNetCore.Identity;
using PTTS.API.Middlewares;
using PTTS.Application;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => { });

// Add Swagger
builder.Services.AddOpenApi();

// Add Authorization and Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

// Add Infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
    app.ApplyMigrations();
}

app.MapGet("/", () => "Hello World!");

// Group Identity API routes under "/auth"
var authGroup = app.MapGroup("/auth").WithTags("Authentication");
authGroup.MapIdentityApi<User>();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
