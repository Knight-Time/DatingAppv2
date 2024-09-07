using API.Controllers;

namespace API.Errors;

public class ApiException(int statusCode, string message , string? details):BaseApiController
{
    public int StatusCode { get; set; } = statusCode;
    public string Message  { get; set; } = message;
    public string? Details { get; set; } = details;



}