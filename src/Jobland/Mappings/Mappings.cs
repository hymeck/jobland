using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;

namespace Jobland.Mappings;

public sealed class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryAddRequest, Category>();
        CreateMap<Category, CategoryListItemDto>()
            .ForMember(d => d.Subcategories, map => map.MapFrom(s => s.Subcategories));
        CreateMap<Category, CategoryDto>();
    }
}

public sealed class SubcategoryMappingProfile : Profile
{
    public SubcategoryMappingProfile() => CreateMap<Subcategory, SubcategoryListItemDto>();
}

public sealed class WorkMappingProfile : Profile
{
    public WorkMappingProfile()
    {
        CreateMap<WorkAddRequest, Work>();
        CreateMap<Work, WorkDto>();
    }
}
