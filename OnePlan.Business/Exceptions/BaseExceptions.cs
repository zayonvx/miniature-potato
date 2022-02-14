using OnePlan.Business.Enums;

namespace OnePlan.Business.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException() {}

    protected BaseException(string userMessage, string systemMessage = null) : base(userMessage)
    {
        SystemMessage = systemMessage;
    }

    public abstract ResponseType ResponseType { get;}

    public string SystemMessage { get; protected set; }

    protected virtual string DefaultMessageTemplate => "{0}";
}
