using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// This class is using to send a message from user A to User B
/// </summary>
public class Message:AuditableEntity
{
    public long FkFromUserId { get;  set; }
    public User FromUser { get;  set; }

    public long FkToUserId { get;  set; }
    public User ToUser { get;  set; }

    public string MessageContent { get;  set; }

    public bool Like { get;  set; }

    public bool IsRead { get;  set; }
}