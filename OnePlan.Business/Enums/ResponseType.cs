using System.Diagnostics;
using System.Net;

namespace OnePlan.Business.Enums;

    public enum ResponseType
    {
        NotImplemented = -1,
        Success = 1,
        NotAuthentificated = 2,
        NotAuthorized = 3,
        NotFound = 4,
        FailedValidation = 5,
        InvalidOperation = 6,
        ExternalRequestFailed = 7,
        Unexpected = 404,
    }

    public static class ResponseTypeExtensions
    {
        public static HttpStatusCode ToHttpStatusCode(this ResponseType code)
        {
            switch (code)
            {
                case ResponseType.NotImplemented:
                    return HttpStatusCode.NotImplemented;
                case ResponseType.Success:
                    return HttpStatusCode.OK;
                case ResponseType.NotAuthentificated:
                    return HttpStatusCode.Unauthorized;
                case ResponseType.NotAuthorized:
                    return HttpStatusCode.Unauthorized;
                case ResponseType.NotFound:
                    return HttpStatusCode.NotFound;
                case ResponseType.FailedValidation:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
