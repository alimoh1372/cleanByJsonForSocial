namespace Application.Common.Exceptions;

public class NotAuthenticatedRequestException:Exception
{
    public NotAuthenticatedRequestException(string message="The request isn't authenticated")
    :base(message)
    {
        
    }
}