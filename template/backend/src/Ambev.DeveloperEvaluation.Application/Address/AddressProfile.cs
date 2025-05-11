using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Address;

public class AddressProfile :Profile
{
    public AddressProfile()
    {
        CreateMap<BaseAddressDto, Domain.Entities.Address>().ReverseMap();
        CreateMap<GeolocationDto, Geolocation>().ReverseMap();
    }
    
}