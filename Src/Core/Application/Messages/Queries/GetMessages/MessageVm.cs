using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Messages.Queries.GetMessages;

/// <summary>
/// A model to show the message on client side
/// </summary>
public class MessageVm:IMapFrom<Message>
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }

    public long FkFromUserId { get; set; }
    public string SenderFullName { get; set; }


    public long FkToUserId { get; set; }

    public string ReceiverFullName { get; set; }

    public string MessageContent { get; set; }

    

    
    //TODO:Add FromUser Image and
    //TODO:Implementing Like And ReadMessage operation

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Message, MessageVm>()
            .ForMember(des => des.SenderFullName, opt =>
                opt.MapFrom(src => src.FromUser.Name + " " + src.FromUser.LastName))
            .ForMember(des => des.ReceiverFullName, opt =>
                opt.MapFrom(src => src.ToUser.Name + " " + src.ToUser.LastName))
            ;

    }
}
