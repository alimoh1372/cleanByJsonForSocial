namespace Application.Common.Exceptions;

public class DuplicationException:Exception
{
    public DuplicationException(string typeName,string duplicatedValue)
    : base($"The \"{typeName}\" is duplicate.duplicated value is \"{duplicatedValue}\"")
    {
        
    }
}