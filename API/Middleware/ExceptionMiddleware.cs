

using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
    IHostEnvironment env)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
           logger.LogError(e,e.Message);
           context.Response.ContentType = "application/json";
           context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

           var response = env.IsProduction()
               ? new ApiException(context.Response.StatusCode, e.Message, "internal server err")
               : new ApiException(context.Response.StatusCode, e.Message, e.StackTrace);

           var options = new JsonSerializerOptions
           {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase
           };

           var json = JsonSerializer.Serialize(response, options);

           await context.Response.WriteAsync(json);

        }
    }
    
    
}







