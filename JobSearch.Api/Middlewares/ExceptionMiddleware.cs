using System.Text;
using System.Threading.Tasks;
using JobSearch.ApplicationCore.Common.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JobSearch.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly ILogger<ExceptionMiddleware> _logger;
        
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        
        private static async Task HandleExceptionAsync(HttpContext context, CustomException exception)
        {
            var bytes = Encoding.UTF8.GetBytes(exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;
            
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = Encoding.Default.GetString(bytes),
            }.ToString());
        }
    }
}