using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;

namespace Jobland.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile() => CreateMap<CategoryAddRequest, Category>();
}
