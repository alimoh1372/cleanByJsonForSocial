using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetAllUsersWithRequestStatus;

/// <summary>
/// An Dto that show user some info with the request statuse
/// </summary>
public class UserWithRequestStatusVm : IMapFrom<User>
{
    //User that request sent to 
    public long UserId { get; set; }
    //User that request sent to
    public string Name { get; set; }

    public string LastName { get; set; }
    public byte[] ProfilePicture { get; set; }
    //Status of request
    public RequestStatus RequestStatusNumber { get; set; }
    public DateTimeOffset TimeOffset { get; set; }

    public string RelationRequestMessage { get; set; }
    public int MutualFriendNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserWithRequestStatusVm>()
            .ForMember(des => des.MutualFriendNumber, opt => opt.Ignore())
            .ForMember(des => des.RelationRequestMessage, opt => opt.Ignore())
            .ForMember(des => des.RequestStatusNumber, opt => opt.Ignore())
            .ForMember(des => des.TimeOffset, opt => opt.Ignore())
            ;
    }
    ////Get all user expect current user
    //var query = await _context.Users.Include(x => x.UserARelations)
    //    .Include(x => x.UserBRelations)
    //    .Where(x => x.Id != currentUserId)
    //    .Select(x => new UserWithRequestStatusVieModel
    //    {
    //        UserId = x.Id,
    //        Name = x.Name,
    //        LastName = x.LastName,
    //        ProfilePicture = x.ProfilePicture

    //    }).AsNoTracking()
    //    .ToListAsync();

}
