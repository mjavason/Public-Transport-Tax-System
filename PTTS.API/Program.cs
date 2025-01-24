using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PTTS.API.Middleware;
using PTTS.Application;
using PTTS.Infrastructure;
using Scalar.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTTS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();
            services.AddAuthentication().AddJwtBearer();
        }

        private static void Configure(WebApplication app)
        {
            app.UseMiddleware<LoggingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
                app.ApplyMigrations();
            }

            app.MapGet("/", () => "Live!");
            app.UseAuthorization();
            app.MapControllers();
        }

        internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
        {
            private readonly Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider _authenticationSchemeProvider;

            public BearerSecuritySchemeTransformer(Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider authenticationSchemeProvider)
            {
                _authenticationSchemeProvider = authenticationSchemeProvider;
            }

            public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
            {
                var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
                if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
                {
                    var requirements = new Dictionary<string, OpenApiSecurityScheme>
                    {
                        ["Bearer"] = new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            In = ParameterLocation.Header,
                            BearerFormat = "Json Web Token"
                        }
                    };
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes = requirements;

                    foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
                    {
                        operation.Value.Security.Add(new OpenApiSecurityRequirement
                        {
                            [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] = Array.Empty<string>()
                        });
                    }
                }
            }
        }
    }
}
