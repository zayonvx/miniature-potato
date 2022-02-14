using OnePlan.Business.Enums;

namespace OnePlan.Business.Exceptions;

public class NotAuthentificatedException : BaseException
{
    public NotAuthentificatedException(string userMessage, string systemMessage = null) : base(userMessage, systemMessage)
    {
        
    }


    public override ResponseType ResponseType => ResponseType.NotAuthentificated;
}
