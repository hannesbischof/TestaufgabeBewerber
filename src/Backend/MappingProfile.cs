using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Domain;

namespace Backend
{
    /// <summary>
    /// AutoMapper profile for mapping between DTOs, domain models, and entity models.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, DomainCategory>().ReverseMap();
            CreateMap<DomainCategory, CategoryDto>().ReverseMap();

            // Product mappings
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, DomainProduct>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer))
                .ReverseMap();
            CreateMap<DomainProduct, ProductDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Manufacturer.Id))
                .ReverseMap();

            // Manufacturer mappings
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
            CreateMap<Manufacturer, DomainManufacturer>().ReverseMap();
            CreateMap<DomainManufacturer, ManufacturerDto>().ReverseMap();
        }
    }
}