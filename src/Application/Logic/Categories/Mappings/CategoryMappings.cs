using AutoMapper;
using Jobland.Application.Logic.Categories.Dtos.Responses;
using Jobland.Domain.Core;

namespace Jobland.Application.Logic.Categories.Mappings;

public sealed class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<Category, CategoryPlainResponse>();
        CreateMap<Category, CategoryRichResponse>();
    }
}
