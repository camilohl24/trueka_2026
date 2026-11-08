using AutoMapper;
using Trueka.Entities;
using Trueka.Services.DTOs;
namespace Trueka.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Offer, OfferDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryFormDto>();
        CreateMap<CategoryFormDto, Category>();
        CreateMap<Item, ItemDto>();
        CreateMap<Item, ItemDetailDto>();
        CreateMap<Item, ItemFormDto>();
        CreateMap<ItemFormDto, Item>();

    }
}
