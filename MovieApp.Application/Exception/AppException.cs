namespace MovieApp.Application.Exception;

public class AppException : System.Exception
{
    public int ErrorCode { get; }

    public readonly List<string> Messages = [];

    protected AppException(string message, int errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public AppException(string message, List<string> messages, int errorCode) : base(message)
    {
        ErrorCode = errorCode;
        Messages = messages;
    }
}