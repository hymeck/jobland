using AutoMapper;
using Jobland.Application.Logic.Subcategories.Dtos.Responses;
using Jobland.Domain.Core;

namespace Jobland.Application.Logic.Subcategories.Mappings;

public class SubcategoryMappings : Profile
{
    public SubcategoryMappings()
    {
        CreateMap<Subcategory, SubcategoryPlainResponse>();
    }
}
