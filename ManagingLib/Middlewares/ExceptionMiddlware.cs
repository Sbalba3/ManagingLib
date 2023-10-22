using ManagingLib.Errors;
using System.Net;
using System.Text.Json;

namespace ManagingLib.Middlewares
{
    public class ExceptionMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _host;

        public ExceptionMiddlware(RequestDelegate next, IHostEnvironment host)
        {
            _next = next;
            _host = host;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/jason";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _host.IsDevelopment() ?
                    new MvcExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new MvcExceptionResponse((int)HttpStatusCode.InternalServerError);
                var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, option);
                await context.Response.WriteAsync(json);
            }

        }
    }
}
