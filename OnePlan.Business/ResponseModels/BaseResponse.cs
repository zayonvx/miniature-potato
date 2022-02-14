using System.Diagnostics;
using OnePlan.Business.Enums;
using OnePlan.Business.Exceptions;

namespace OnePlan.Business.ResponseModels;

public class BaseResponse
{
    public BaseResponse()
    {
        GeneratedAtUtc = DateTime.UtcNow;
    }

    public BaseResponse(ResponseType status) : this()
    {
        Status = status;
    }

    public BaseResponse(ResponseType status, string userMessage, string systemMessage = null, string debugInfo= null) : this(status)
    {
        UserMessage = userMessage;
        SystemMessage = systemMessage;
        DebugInfo = debugInfo;
    }

    public BaseResponse(BaseException exception) : this(exception.ResponseType, exception.Message, exception.SystemMessage)
    {
    }

    public BaseResponse(Exception exception) : this(ResponseType.Unexpected, exception.Message, exception.GetType().FullName)
    {
        StackTrace = exception.StackTrace;
    }
    
    public ResponseType Status { get; set; }
    public string UserMessage { get; set; }
    public string SystemMessage { get; set; }
    public string StackTrace { get; set; }
    public string DebugInfo { get; set; }
    public DateTime GeneratedAtUtc { get; set; }
}
