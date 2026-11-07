using AutoMapper;
using Trueka.Entities;
using Trueka.Services.DTOs;
namespace Trueka.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Offer, OfferDto>();
    }
}
