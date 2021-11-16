using AutoMapper;
using Domain;

namespace Application.Common.Subcategories
{
    public class SubcategoryMappingProfile : Profile
    {
        public SubcategoryMappingProfile()
        {
            CreateMap<Subcategory, SubcategoryDto>()
                .ForMember(d => d.SubcategoryId, map => map.MapFrom(s => s.Id))
                .ForMember(d => d.Category, map => map.MapFrom(s => s.ParentCategory));
        }
    }
}
