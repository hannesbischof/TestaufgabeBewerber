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
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
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
                .ReverseMap();
            CreateMap<DomainProduct, ProductDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ReverseMap();
        }
    }
}
