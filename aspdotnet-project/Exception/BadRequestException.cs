namespace aspdotnet_project.Exception;

public class BadRequestException : AppException
{
    public BadRequestException(string message, List<string> messages) : base(message, messages, 400)
    {
    }
    
    public BadRequestException(string message) : base(message, 400)
    {
    }
}