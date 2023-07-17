using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;


/// <summary>
/// User Entity to handling the user operations
/// </summary>
public class User:AuditableEntity
{

    public User()
    {
        UserARelations = new HashSet<UserRelation>();
        UserBRelations = new HashSet<UserRelation>();
        FromMessages = new HashSet<Message>();
        ToMessages = new HashSet<Message>();
    }
    #region Properties
    public long  CustomerId { get; set; }
    public string Name { get; set; }
    public string LastName { get;  set; }
    public Email Email { get;  set; }
    public DateTime BirthDay { get;  set; }
    public string Password { get;  set; }
    public string AboutMe { get;  set; }
    public byte[] ProfilePicture { get;  set; }


    //To create self referencing many-to-many UserRelations
    public ICollection<UserRelation> UserARelations { get; private set; }
    public ICollection<UserRelation> UserBRelations { get; private set; }


    //To  Create self referencing many-to-many message
    public ICollection<Message> FromMessages { get; private set; }
    public ICollection<Message> ToMessages { get; private set; }

    #endregion
}