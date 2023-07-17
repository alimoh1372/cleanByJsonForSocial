using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetEditProfilePicture;

/// <summary>
/// To edit profile using this command
/// </summary>
public class EditProfilePictureVm:IMapFrom<User>
{

    public long Id { get; set; }
    
    public byte[] ProfilePicture { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, EditProfilePictureVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId));
    }

    /*
     * 
     * A good example of how AutoMapper can help.
     * 
    public static Expression<Func<Customer, CustomerDetailVm>> Projection
    {
        get
        {
            return customer => new CustomerDetailVm
            {
                Id = customer.CustomerId,
                Address = customer.Address,
                City = customer.City,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Country = customer.Country,
                Fax = customer.Fax,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode,
                Region = customer.Region
            };
        }
    }

    public static CustomerDetailVm Create(Customer customer)
    {
        return Projection.Compile().Invoke(customer);
    }
    */
}
