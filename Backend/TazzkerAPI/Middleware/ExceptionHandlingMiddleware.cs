
using System.Diagnostics;
using System.Text.Json;

namespace TazzkerAPI.Middleware
{
    public class ErrorResponse
    {
        public string Error { get; set; } = null!;
        public string? StackTrace { get; set; }
    }


    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = _env.IsDevelopment()
                   ? new ErrorResponse
                   {
                       Error = ex.Message,
                       StackTrace = ex.StackTrace
                   }
                   : new ErrorResponse
                   {
                       Error = "Internal Server Error"
                   };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
