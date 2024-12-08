using Newtonsoft.Json;
using System.Net;

namespace SocialNetwork.Exceptions;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var data = error.Message;

            if (error is BaseException exception)
            {
                _logger.LogError(JsonConvert.SerializeObject(exception));
                switch (exception.Code)
                {
                    case ResponseCode.NotAuthorized:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ResponseCode.AccessDenied:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case ResponseCode.NotFound:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ResponseCode.AlreadyExists:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case ResponseCode.ValidationInvalid:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ResponseCode.BadRequest:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                }
            }

            _logger.LogError(JsonConvert.SerializeObject(error));
            var result = JsonConvert.SerializeObject(data);
            await response.WriteAsync(result);
        }
    }
}