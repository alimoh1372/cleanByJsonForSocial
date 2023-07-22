using Domain.Common;

namespace Domain.Entities;
/// <summary>
/// The Entity and valueObject to handle the Relation of users between
/// </summary>
public class UserRelation:AuditableEntity
{
    public long UserRelationId { get; set; }
    public long FkUserAId { get;  set; }
    public User UserA { get;  set; }

    public long FkUserBId { get;  set; }
    public User UserB { get; }

    public string RelationRequestMessage { get; set; }
    public bool Approve { get; set; }

    public UserRelation()
    {
        Approve = false;
    }
}