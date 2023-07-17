using System.Security.Cryptography.X509Certificates;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetEditProfilePicture;

public class GetEditProfilePictureQeuryHandler:IRequestHandler<GetEditProfilePictureQuery,EditProfilePictureVm>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IMapper _mapper;

    public GetEditProfilePictureQeuryHandler(ISocialNetworkDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EditProfilePictureVm> Handle(GetEditProfilePictureQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(request.Id);
        if (entity == null)
            throw new NotFoundException(nameof(User),request.Id);

        return _mapper.Map<EditProfilePictureVm>(entity);
    }
}