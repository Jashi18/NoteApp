using System.Net;
using System.Text.Json;

namespace NoteApp.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                message = "Not found";
                status = HttpStatusCode.NotFound;
            }
            else
            {
                message = exception.Message;
                status = HttpStatusCode.InternalServerError;
            }

            var result = JsonSerializer.Serialize(new { error = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            _logger.LogError(exception, "An unhandled exception has occurred: {Message}", message);

            return context.Response.WriteAsync(result);
        }
    }
}
