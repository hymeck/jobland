using System.Threading;
using System.Threading.Tasks;
using Application.Common.Works;
using Application.Dao;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Commands.Works
{
    public sealed class CreateWorkCommandHandler : IRequestHandler<CreateWorkCommand, WorkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDaoAsync<Work> _workDao;

        public CreateWorkCommandHandler(IMapper mapper, IDaoAsync<Work> workDao)
        {
            _mapper = mapper;
            _workDao = workDao;
        }

        public async Task<WorkResponse> Handle(CreateWorkCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateWorkCommand, Work>(request);
            var work = await _workDao.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<Work?, WorkDto?>(work);
            return new WorkResponse(dto);
        }
    }
}
