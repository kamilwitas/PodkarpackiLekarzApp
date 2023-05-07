using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Infrastructure.Exceptions;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException ex)
        {
            var jsonResponse = HandleException(context, ex, 400);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (AuthorizationException ex)
        {
            var jsonResponse = HandleException(context, ex, 400);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (ValidationErrorException ex)
        {
            var jsonResponse = HandleValidationErrorException(context, ex, 400);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (ForbiddenException ex)
        {
            var jsonResponse = HandleException(context, ex, 403);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (NotFoundException ex)
        {
            var jsonResponse = HandleException(context, ex, 404);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.Message);
        }
    }

    private string HandleException(HttpContext context, Exception ex, int httpStatusCode)
    {
        _logger.LogError(ex, ex.Message);

        context.Response.StatusCode = httpStatusCode;
        context.Response.Headers.Add("content-type", "application/json");

        var errorCode = ToUnderscoreCase(ex.GetType().Name.Replace("Exception", string.Empty));
        var json = JsonConvert.SerializeObject(new { ErrorCode = errorCode, ErrorMessage = ex.Message });

        return json;
    }

    private string HandleValidationErrorException(HttpContext context, ValidationErrorException ex, int httpStatusCode)
    {
        _logger.LogError(ex, ex.Message);

        context.Response.StatusCode = httpStatusCode;
        context.Response.Headers.Add("content-type", "application/json");

        var errorCode = ToUnderscoreCase(ex.GetType().Name.Replace("Exception", string.Empty));

        var json = JsonConvert.SerializeObject(new { ErrorCode = errorCode, Errors = ex.ValidationErrors });

        return json;
    }

    public static string ToUnderscoreCase(string value)
            => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i - 1]) ? $"_{x}" : x.ToString())).ToLower();

}