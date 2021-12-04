using AutoMapper;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Domain.Core;

namespace Jobland.Application.Logic.Works.Mappings;

public class WorkMappings : Profile
{
    public WorkMappings()
    {
        CreateMap<AddWorkRequest, Work>();

        // todo: add map rules of related entities explicitly
        CreateMap<Work, WorkPlainResponse>();

        CreateMap<UpdateWorkRequest, Work>();
    }
}
