using ECommerceTask.API.Models;
using System.Net;

namespace ECommerceTask.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata oluştu: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sunucu kaynaklı bir hata oluştu. Lütfen daha sonra tekrar deneyiniz."
                
            };

            await context.Response.WriteAsync(response.ToString());
        }
    }
}