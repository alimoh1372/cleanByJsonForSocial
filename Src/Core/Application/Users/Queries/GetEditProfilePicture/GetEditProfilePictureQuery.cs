using MediatR;

namespace Application.Users.Queries.GetEditProfilePicture;

public class GetEditProfilePictureQuery:IRequest<EditProfilePictureVm>
{
    public long Id { get; set; }

}