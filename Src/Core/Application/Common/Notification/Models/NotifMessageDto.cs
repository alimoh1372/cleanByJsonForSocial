namespace Application.Common.Notification.Models;

public class NotifMessageDto
{
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}