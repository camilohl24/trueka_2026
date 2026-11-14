using Microsoft.AspNetCore.Diagnostics;
namespace Trueka.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var status = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status403Forbidden,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            _ => 0
        };
        if (status == 0) return false;
        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new { message = exception.Message }, cancellationToken);
        return true;


    }
}
