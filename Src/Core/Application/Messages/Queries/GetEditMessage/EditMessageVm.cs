using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Messages.Queries.GetEditMessageQuery;

/// <summary>
/// Command model of Edit message
/// </summary>
public class EditMessageVm:IMapFrom<Message>
{
   
    public long Id { get; set; }
  
    public long FkFromUserId { get; set; }

    public long FkToUserId { get; set; }
    
    public string MessageContent { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Message, EditMessageVm>();
    }
}
