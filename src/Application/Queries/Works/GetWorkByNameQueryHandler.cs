using System.Threading;
using System.Threading.Tasks;
using Application.Common.Works;
using Application.Dao;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Queries.Works
{
    public sealed class GetWorkByNameQueryHandler : IRequestHandler<GetWorkByNameQuery, WorkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkDao _workDao;

        public GetWorkByNameQueryHandler(IMapper mapper, IWorkDao workDao)
        {
            _mapper = mapper;
            _workDao = workDao;
        }

        public async Task<WorkResponse> Handle(GetWorkByNameQuery request, CancellationToken cancellationToken)
        {
            var work = await _workDao.FindByNameAsync(request.WorkName, cancellationToken);
            var dto = _mapper.Map<Work?, WorkDto?>(work);
            return new WorkResponse(dto);
        }
    }
}
