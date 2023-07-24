using LivrariaFuturo.Authorization.Core.Domain;
using Newtonsoft.Json;
using System.Net;

namespace LivrariaFuturo.Authentication.Api.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var statusCode = (int)HttpStatusCode.InternalServerError;

            response.ContentType = "application/json";
            response.StatusCode = statusCode;

            return response.WriteAsync(JsonConvert.SerializeObject(new InternalServerErrorApiResponse(exception)));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware(typeof(CustomExceptionHandlerMiddleware));
    }

    public class CustomErrorResponse
    {
        public string Message       { get; set; }
        public string Description   { get; set; }
        public int StatusCode       { get; set; }
    }
}