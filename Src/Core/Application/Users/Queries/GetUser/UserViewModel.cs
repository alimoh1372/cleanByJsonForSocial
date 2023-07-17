using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Users.Queries.GetUser;

/// <summary>
/// A Dto to map the user entity to client side 
/// </summary>
public class UserViewModel:IMapFrom<User>
{
    public long Id { get; set; }
    public Email Email { get; set; }
    public byte[] ProfilePicture { get; set; }
    public string Name { set; get; }
    public string LastName { set; get; }
    public string AboutMe { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User,UserViewModel>()
            .ForMember(u=>u.Id,opt=> opt.MapFrom(x=>x.UserId));
    }
}