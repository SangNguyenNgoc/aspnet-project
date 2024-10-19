namespace MovieApp.Application.Exception;

public class DataNotFoundException : AppException
{
    public DataNotFoundException(string message) : base(message, 404)
    {
    }

    public DataNotFoundException(string message, List<string> messages) : base(message, messages, 404)
    {
    }
}