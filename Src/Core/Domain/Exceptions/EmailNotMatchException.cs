namespace Domain.Exceptions;

public class EmailNotMatchException:Exception
{
    public EmailNotMatchException(string email,Exception ex):base(
        $"Email\"{email}\" not an email format.",ex)
    {
        
    }
    public EmailNotMatchException(string email) : base(
        $"Email\"{email}\" not an email format.")
    {

    }
}