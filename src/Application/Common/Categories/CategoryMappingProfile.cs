using AutoMapper;
using Domain;

namespace Application.Common.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.CategoryId, map => map.MapFrom(s => s.Id));
        }
    }
}