using CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Dtos;
using Engine.Mongo;
using MapsterMapper;
using MediatR;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
public class CreateCarRacingTeamCommandHandler : IRequestHandler<CreateCarRacingTeamCommand, CreateCarRacingTeamResponseDto>
{
    private readonly IMongoRepository<Models.CarRacingTeam, Guid> _repository;
    private readonly IMapper _mapper;

    public CreateCarRacingTeamCommandHandler(IMongoRepository<Models.CarRacingTeam, Guid> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<CreateCarRacingTeamResponseDto> Handle(CreateCarRacingTeamCommand request, CancellationToken cancellationToken)
    {
        var carRacingTeam = Models.CarRacingTeam.Create(request.Name, request.Country);
        await _repository.AddAsync(carRacingTeam, cancellationToken);

        return _mapper.Map<CreateCarRacingTeamResponseDto>(carRacingTeam);
    }
}
