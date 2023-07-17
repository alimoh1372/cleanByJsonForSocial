using Application.Common.Utility;

namespace Application.Common.Exceptions;

public class NotAuthorizedRequestException:Exception
{
    public NotAuthorizedRequestException(string message=ApplicationMessage.NotAuthorized)
    :base(message)
    {
        
    }    
}