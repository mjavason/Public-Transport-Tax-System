using System.Net;
using PTTS.API.Filters.Model;

namespace PTTS.API.Middlewares;

public class ExceptionMiddleware
{

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await _next(context);

            //Handle 404 Error when no matching route is found
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound && context.Response.HasStarted == false)
            {
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Status = context.Response.StatusCode,
                    Message = "Resource not found.",
                    Type = nameof(HttpStatusCode.NotFound),
                });
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, ILogger<ExceptionMiddleware> logger)
    {

        var statusCode = HttpStatusCode.InternalServerError;
        var problemDetails = new ErrorResponse
        {
            Status = (int)statusCode,
        };

        switch (ex)
        {
            default:
                problemDetails.Message = ex.Message;
                problemDetails.Type = nameof(HttpStatusCode.InternalServerError);
                logger.LogError("Something went wrong. Please contact support : {@problemDetails}", problemDetails);
                problemDetails.Message = "Something went wrong. Please contact support";
                break;
        }
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
}