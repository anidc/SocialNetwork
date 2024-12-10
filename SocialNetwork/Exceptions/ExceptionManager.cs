using SocialNetwork.Exceptions;

namespace FirstCast.Application.Services;

public static class ExceptionManager
{
    public static BaseException NotAuthorized()
        => GetException(ResponseCode.NotAuthorized, "User is not authorized");

    public static BaseException AccessDenied()
        => GetException(ResponseCode.AccessDenied, "No access to perform this action");

    public static BaseException NotFound(string entityName, string? id = null)
        => GetException(ResponseCode.NotFound, string.Concat(entityName, " ", id, " ", "Not Found"));

    public static BaseException AlreadyExists(string entityName, string id)
        => GetException(ResponseCode.AlreadyExists, string.Concat(entityName, " ", id, " ", "Already exists"));

    public static BaseException BadRequest(string reason) => GetException(ResponseCode.BadRequest, reason);

    public static BaseException Validation(string entityName, string field)
        => GetException(ResponseCode.ValidationInvalid, string.Concat(entityName, " ", field, " ", "Not Valid"));

    private static BaseException GetException(ResponseCode responseCodes, string defaultMessage)
    {
        return new BaseException(responseCodes, defaultMessage);
    }
}