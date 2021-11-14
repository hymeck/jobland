using System.Threading;
using System.Threading.Tasks;
using Application.Dao;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Commands.Works
{
    public sealed class CreateWorkCommandHandler : IRequestHandler<CreateWorkCommand, CreateWorkCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDaoAsync<Work> _workDao;

        public CreateWorkCommandHandler(IMapper mapper, IDaoAsync<Work> workDao)
        {
            _mapper = mapper;
            _workDao = workDao;
        }

        public async Task<CreateWorkCommandResponse> Handle(CreateWorkCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateWorkCommand, Work>(request);
            var newWork = await _workDao.AddAsync(entity, cancellationToken);
            // todo: handle two options: value and null
            return _mapper.Map<Work, CreateWorkCommandResponse>(newWork);
        }
    }
}
